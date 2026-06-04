
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Mfa;
using MOS.Application.DTOs.Responses.Products;
using MOS.Application.Exceptions;
using MOS.Application.ExternalServices.AuthInterfaces;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MOS.Infrastructure.ExternalServices.AuthImplements
{
    public class MicrosoftService : IMicrosoftService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        private readonly string _microsoftGraphApiBaseUrl = "https://login.microsoftonline.com/";
        private readonly string _microsoftExtendApiBaseUrl = "/oauth2/v2.0/";
        private readonly string _microsoftGetInforUrl = "https://graph.microsoft.com/v1.0/me";

        public MicrosoftService(
            IConfiguration configuration,
            HttpClient httpClient,
            ITokenService tokenService,
            IUserRepository userRepository,
            IPermissionRepository permissionRepository,
            IAuditRepository auditRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _auditRepository = auditRepository;
            _mapper = mapper;
        }

        public string BuildMicrosoftAuthUrl(string state)
        {
            var clientId = _configuration["MicrosoftOAuth:ClientId"]!;
            var tenantId = _configuration["MicrosoftOAuth:TenantId"]!;
            var redirectUri = _configuration["MicrosoftOAuth:RedirectUri"]!;
            var queryParams = new Dictionary<string, string>
            {
                ["state"] = state,
                ["client_id"] = clientId,
                ["redirect_uri"] = redirectUri,
                ["scope"] = "openid profile email User.Read",
                ["response_type"] = "code",
                ["response_mode"] = "query",
            };

            var queryString = string.Join("&", queryParams
                .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

            return _microsoftGraphApiBaseUrl + tenantId + _microsoftExtendApiBaseUrl + "authorize?" + queryString;

        }

        public async Task<AuthResponse> HandleMicrosoftCallbackAsync(string code)
        {
            var exchangeToken = await ExchangeCodeForTokensAsync(code);

            // Step B — call Graph /me with access token
            var accessToken = exchangeToken.AccessToken;
            var authReponse = await GetUserProfileAsync(accessToken);

            var existUser = await _userRepository.GetUserByEmailAsync(authReponse.Email);
            if (existUser == null)
            {
                authReponse.RequiresRegistration = true;
                return authReponse;
            }

            authReponse.Id = existUser.Id;
            authReponse.SigninMethod = existUser.SigninMethod;
            authReponse.Status = existUser.Status;
            authReponse.Role = existUser.Role;
            authReponse.Token = _tokenService.GenerateToken(authReponse);

            var products = await _permissionRepository.GetProductsByUserIdAsync(existUser.Id);
            authReponse.Products = products.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            await LogAudit(authReponse);

            return authReponse;
        }



        private async Task<MicrosoftAuthResponse> ExchangeCodeForTokensAsync(string code)
        {
            var clientId = _configuration["MicrosoftOAuth:ClientId"]!;
            var clientSecret = _configuration["MicrosoftOAuth:ClientSecret"]!;
            var tenantId = _configuration["MicrosoftOAuth:TenantId"]!;
            var redirectUri = _configuration["MicrosoftOAuth:RedirectUri"]!;

            var body = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["code"] = code,
                ["redirect_uri"] = redirectUri,
                ["grant_type"] = "authorization_code",
                ["scope"] = "openid profile email User.Read"
            });

            var http = new HttpClient();
            var response = await http.PostAsync(_microsoftGraphApiBaseUrl + tenantId + _microsoftExtendApiBaseUrl + "token", body);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ConflictException("Microsoft", "Exchange Token Failed With: " + error);
            }

            var json = await response.Content.ReadAsStringAsync();
            var exchangeToken = JsonSerializer.Deserialize<MicrosoftAuthResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (exchangeToken == null) throw new ConflictException("Microsoft", "Serializer Exchange Token");

            return exchangeToken;
        }

        private async Task<AuthResponse> GetUserProfileAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _microsoftGetInforUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new ConflictException("Microsoft", "Get User Profile From Microsoft Failed With: " + errorBody);
            }


            var json = await response.Content.ReadAsStringAsync();
            var claims = JsonSerializer.Deserialize<JsonElement>(json);

            var userProfile = new AuthResponse
            {
                UserName = claims.GetProperty("displayName").GetString() ?? string.Empty,
                Name = claims.GetProperty("givenName").GetString() + claims.GetProperty("surname").GetString(),
                Email = ExtractEmail(claims),
                Phone = claims.GetProperty("mobilePhone").GetString() ?? string.Empty,
                SigninMethod = SigninMethod.microsoft,

            };

            return userProfile;
        }

        private string ExtractEmail(JsonElement claims)
        {
            var mail = claims.GetProperty("mail").GetString();
            if (!string.IsNullOrEmpty(mail)) return mail;

            var upn = claims.GetProperty("userPrincipalName").GetString();

            if (upn == null) throw new ConflictException("User Email From Microsoft", "Not Found");

            if (upn.Contains("#EXT#"))
            {
                var extracted = upn.Split("#EXT#")[0];
                var index = extracted.IndexOf('_');
                return index >= 0
                    ? extracted.Substring(0, index) + "@" + extracted.Substring(index + 1)
                    : extracted;
            }

            return upn;
        }


        private async Task LogAudit(AuthResponse authReponse)
        {
            await _auditRepository.AddAsync(
           new AuditLog(
           authReponse.Id,
           authReponse.Name,
           authReponse.UserName,
           CategoryLogType.Account.ToString(),
           authReponse.Email,
           AuditAction.SignIn,
           $"User {authReponse.Email} login via Microsoft")
          );
        }
    }
}
