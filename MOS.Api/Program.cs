// ─────────────────────────────────────
// Using Statements
// ─────────────────────────────────────
// TODO: add using statements as you implement each section

using FluentValidation;
using FluentValidation.AspNetCore;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using MOS.Api.Extentions;
using MOS.Api.Filters;
using MOS.Api.Middleware;
using MOS.Application.ExternalServices.AuthInterfaces;
using MOS.Application.ExternalServices.SecurityInterfaces;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Application.Validators.Audit;
using MOS.Application.Validators.Auth;
using MOS.Application.Validators.Users;
using MOS.Domain.Constants;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Db.Seeds;
using MOS.Infrastructure.ExternalServices.AuthImplements;
using MOS.Infrastructure.ExternalServices.Email;
using MOS.Infrastructure.ExternalServices.EmailImplements;
using MOS.Infrastructure.ExternalServices.SecurityImplements;
using MOS.Infrastructure.Implements;
using MOS.Infrastructure.Interfaces;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;
using static MOS.Domain.Entities.Tenant;


var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// ─────────────────────────────────────
//  Controllers + Filters
// ─────────────────────────────────────
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// ─────────────────────────────────────
// Swagger with JWT support
// ─────────────────────────────────────

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();

        policy.RequireClaim("scope", "mos_api"); //Using for 3rd api checking later
    });
    options.AddPolicy(Permissions.AdminPolicy, //Allow to reuse the policy
        policy => policy.RequireRole("Administrator"));
    options.AddPolicy(Permissions.TenantUserPolicy,
        policy => policy.RequireRole("TenantUser"));
    options.AddPolicy(Permissions.TenantAdministratorPolicy,
       policy => policy.RequireRole("TenantAdministrator"));
});

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "MOS API";
    config.Version = "v1";
    config.AddSecurity("Bearer", Enumerable.Empty<string>(),
         new OpenApiSecurityScheme
         {
             Type = OpenApiSecuritySchemeType.Http,
             Scheme = "bearer",
             BearerFormat = "JWT",
             Description = "Enter JWT token only"
         });

    config.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});


// ─────────────────────────────────────
// Database (Change different connection strings for different developers/environments)
// ─────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));


// ─────────────────────────────────────
// Repositories
// ─────────────────────────────────────
builder.Services.AddScoped<IMfaRepository, MfaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IEmailWhitelistRepository, EmailWhitelistRepository>();

// ─────────────────────────────────────
// External Services
// ─────────────────────────────────────
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// ─────────────────────────────────────
// Application Services
// ─────────────────────────────────────
builder.Services.AddScoped<IMfaService, MfaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IGoogleService, GoogleService>();
builder.Services.AddScoped<IEmailWhitelistService, EmailWhitelistService>();
builder.Services.AddHttpClient<IMicrosoftService, MicrosoftService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//─────────────────────────────────────
// Other Services
//─────────────────────────────────────

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantProvider>();
builder.Services.AddScoped<ITenantGetter>(sp => sp.GetRequiredService<TenantProvider>());
builder.Services.AddScoped<ITenantSetter>(sp => sp.GetRequiredService<TenantProvider>());
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // state only needs to live briefly
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// ─────────────────────────────────────
//  Policy
// ─────────────────────────────────────
builder.Services.AddCustomCorsPolicy();

// ─────────────────────────────────────
//  Seeders
// ─────────────────────────────────────
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<ProductSeeder>();
builder.Services.AddScoped<AdminSeeder>();
builder.Services.AddScoped<EmailWhitelistSettingSeeder>();

// ─────────────────────────────────────
// FluentValidation
// ─────────────────────────────────────
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserQueryRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BatchCreateUserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserProductPermissionsRequestValidator>();

// ─────────────────────────────────────
//  JWT Authentication
// ─────────────────────────────────────
builder.Services.AddJwt(null, builder.Configuration);
builder.Services.Configure<TokenSetting>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<ITokenService, TokenService>();

// ─────────────────────────────────────
//  log4net
// ─────────────────────────────────────
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// ─────────────────────────────────────
// Gmail Api
// ─────────────────────────────────────
builder.Services.Configure<GmailApiSetting>(
    builder.Configuration.GetSection("GmailApi"));

// ─────────────────────────────────────
// Google Outh
// ─────────────────────────────────────
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["GoogleOAuth:ClientId"]!;
    options.ClientSecret = builder.Configuration["GoogleOAuth:ClientSecret"]!;
    options.CallbackPath = builder.Configuration["GoogleOAuth:UrlCallBack"]!;
    //options.Scope.Add("openid");
    //options.Scope.Add("profile");
    //options.Scope.Add("email");
    options.SaveTokens = true;

    // Explicitly map fields ASP.NET skips by default
    options.ClaimActions.MapJsonKey("email_verified", "email_verified");
    options.ClaimActions.MapJsonKey("picture", "picture");
    options.ClaimActions.MapJsonKey("locale", "locale");
});


// ─────────────────────────────────────
// Build App
// ─────────────────────────────────────
var app = builder.Build();
app.UserPerformanceLogging();

//// ─────────────────────────────────────
//// Auto Migration To Latest Version on Startup (Use with caution in production, consider using manual migrations instead)
//// ─────────────────────────────────────
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider
//        .GetRequiredService<AppDbContext>();

//    db.Database.Migrate();
//}

// ─────────────────────────────────────
// Middleware Pipeline — ORDER MATTERS
// ─────────────────────────────────────

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSession();

//Uncomment for real production demo
//if (app.Environment.IsDevelopment())
//{
//    app.UseOpenApi();

//    app.UseSwaggerUi(config =>
//    {
//        config.DocumentTitle = "MOS API";
//    });

//    app.UseDebugContext();
//}

// Use for testing in production
app.UseOpenApi();

app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "MOS API";
});

if (app.Environment.IsDevelopment())
{
    app.UseDebugContext();
}

app.UseExceptionHandling();
app.UseRequestLogging();
app.UseHttpsRedirection();
app.UseCors();
app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ─────────────────────────────────────
// Run Seeders on Startup
// ─────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    try
    {

        var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during seeding: {ex.Message}");
    }
}

app.Run();