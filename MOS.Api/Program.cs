// ─────────────────────────────────────
// Using Statements
// ─────────────────────────────────────
// TODO: add using statements as you implement each section

using FluentValidation;
using FluentValidation.AspNetCore;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using MOS.Api.Extentions;
using MOS.Api.Filters;
using MOS.Api.Middleware;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Application.Validators.Auth;
using MOS.Application.Validators.Users;
using MOS.Domain.Constants;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Db.Seeds;
using MOS.Infrastructure.ExternalServices.Email;
using MOS.Infrastructure.ExternalServices.Security;
using MOS.Infrastructure.ExternalServices.Security.Implements;
using MOS.Infrastructure.ExternalServices.SecurityImplements;
using MOS.Infrastructure.Implements;
using MOS.Infrastructure.Interfaces;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;
using static MOS.Domain.Entities.Tenant;


var builder = WebApplication.CreateBuilder(args);

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
//builder.Configuration.GetConnectionString("Product_Connection")));
builder.Configuration.GetConnectionString("Kris_Dev_Local_Connection")));
//builder.Configuration.GetConnectionString("Trevor_Dev_Local_Connection")));

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
builder.Services.AddScoped<IEmailWhiteListService, EmailWhitelistService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//─────────────────────────────────────
// Other Services
//─────────────────────────────────────

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantProvider>();
builder.Services.AddScoped<ITenantGetter>(sp => sp.GetRequiredService<TenantProvider>());
builder.Services.AddScoped<ITenantSetter>(sp => sp.GetRequiredService<TenantProvider>());
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

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
// Build App
// ─────────────────────────────────────
var app = builder.Build();
app.UserPerformanceLogging();
// ─────────────────────────────────────
// Middleware Pipeline — ORDER MATTERS
// ─────────────────────────────────────

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "MOS API";
    });

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