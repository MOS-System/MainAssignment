using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Mfa;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Mfa;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Infrastructure.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace MOS.Application.Services.Implements
{
    // generate code, validate, expire
    public class MfaService : BaseService<MfaService>, IMfaService
    {
        private readonly IMfaRepository _IMfaRepository;
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly string _microsoftGraphApiBaseUrl = "https://login.microsoftonline.com/";
        private readonly string _microsoftExtendApiBaseUrl = "/oauth2/v2.0/";
        private readonly string _microsoftGetInforUrl = "https://graph.microsoft.com/v1.0/me";

        public MfaService(
            IMfaRepository mfaRepository,
            HttpClient httpClient,
            ITokenService tokenService,
            ILogger<MfaService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _IMfaRepository = mfaRepository;
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public string BuildMicrosoftAuthUrl(string state)
        {
            var clientId = _configuration["MicrosoftOAuth:ClientId"]!;
            var tenantId = _configuration["MicrosoftOAuth:TenantId"]!;
            var redirectUri = _configuration["MicrosoftOAuth:RedirectUri"]!;

            var queryParams = new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["response_type"] = "code",
                ["redirect_uri"] = redirectUri,
                ["scope"] = "openid profile email User.Read",
                ["state"] = state,
                ["response_mode"] = "query"
            };

            var queryString = string.Join("&", queryParams
                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            return _microsoftGraphApiBaseUrl + tenantId + _microsoftExtendApiBaseUrl + "authorize?" + queryString;

        }

        public async Task<MicrosoftAuthRequest> HandleMicrosoftCallbackAsync(string code)
        {
            var tokenResponse = await ExchangeCodeForTokensAsync(code);
            if (tokenResponse == null) throw new ConflictException("Microsoft", "Serializer Exchange Token");

            // Step B — call Graph /me with access token
            var accessToken = tokenResponse.AccessToken;
            var userInfo = await GetUserProfileAsync(accessToken);
            if (userInfo == null) throw new NotFoundException("Microsoft", "User Profile");

            // tid still comes from IdToken since Graph /me doesn't expose it
            var tenantId = GetTenantIdFromIdToken(tokenResponse.IdToken);

            return new MicrosoftAuthRequest
            {
                Token = null,
                Email = userInfo.Email,
                DisplayName = userInfo.DisplayName,
                TenantId = tenantId
            };
        }
        private async Task<MicrosoftAuthResponse> ExchangeCodeForTokensAsync(string code)
        {
            var clientId = _configuration["MicrosoftOAuth:ClientId"]!;
            var clientSecret = _configuration["MicrosoftOAuth:ClientSecret"]!;
            var tenantId = _configuration["MicrosoftOAuth:TenantId"]!;
            var redirectUri = _configuration["MicrosoftOAuth:RedirectUri"]!;

            var body = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["code"] = code,
                ["redirect_uri"] = redirectUri,
                ["scope"] = "openid profile email User.Read"
            });

            var http = new HttpClient();
            var response = await http.PostAsync(_microsoftGraphApiBaseUrl + tenantId + _microsoftExtendApiBaseUrl + "token", body);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new ConflictException("Microsoft", "Exchange Token Failed With: " + errorBody);
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MicrosoftAuthResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task<MicrosoftUserInfo> GetUserProfileAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _microsoftGetInforUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new ConflictException("Microsoft", "Get User Infor From Microsoft Failed With: " + errorBody);
            }


            var json = await response.Content.ReadAsStringAsync();
            var claims = JsonSerializer.Deserialize<JsonElement>(json);

            return new MicrosoftUserInfo
            {
                Email = claims.GetProperty("mail").GetString()
                              ?? claims.GetProperty("userPrincipalName").GetString(), // fallback
                DisplayName = claims.GetProperty("displayName").GetString(),
                TenantId = null, // Graph /me doesn't return tid, keep reading
                ObjectId = claims.GetProperty("id").GetString()
            };
        }

        private string GetTenantIdFromIdToken(string idToken)
        {
            try
            {
                var payload = idToken.Split('.')[1];
                payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                var claims = JsonSerializer.Deserialize<JsonElement>(json);
                return claims.GetProperty("tid").GetString();
            }
            catch { return null; }
        }
        public class MicrosoftUserInfo
        {
            public string Email { get; set; }
            public string DisplayName { get; set; }
            public string TenantId { get; set; }
            public string ObjectId { get; set; }
        }
    }
}
