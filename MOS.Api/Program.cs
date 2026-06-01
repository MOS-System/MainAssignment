// ─────────────────────────────────────
// Using Statements
// ─────────────────────────────────────
// TODO: add using statements as you implement each section

using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOS.Api.Filters;
using MOS.Api.Middleware;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Db.Seeds;
using MOS.Infrastructure.ExternalServices.Email.Implements;
using MOS.Infrastructure.ExternalServices.Security.Implements;
using MOS.Infrastructure.Repositories.Implements;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────
// 1. Controllers + Filters
// ─────────────────────────────────────
builder.Services.AddControllers(options =>
{
   options.Filters.Add<ValidationFilter>();
});

// ─────────────────────────────────────
// 2. Swagger with JWT support
// ─────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ─────────────────────────────────────
// 3. Database (Change different connection strings for different developers/environments)
// ─────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(
//builder.Configuration.GetConnectionString("Product_Connection")));
//builder.Configuration.GetConnectionString("Kris_Dev_Local_Connection")));
builder.Configuration.GetConnectionString("Trevor_Dev_Local_Connection")));

// ─────────────────────────────────────
// 4. Repositories
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
// 5. External Services
// ─────────────────────────────────────
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// ─────────────────────────────────────
// 6. Application Services
// ─────────────────────────────────────
builder.Services.AddScoped<IMfaService, MfaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailWhiteListService, EmailWhitelistService>();

// ─────────────────────────────────────
// 7. Seeders
// ─────────────────────────────────────
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<ProductSeeder>();
builder.Services.AddScoped<AdminSeeder>();
builder.Services.AddScoped<EmailWhitelistSettingSeeder>();

// ─────────────────────────────────────
// 8. FluentValidation
// ─────────────────────────────────────
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

// ─────────────────────────────────────
// 9. JWT Authentication
// ─────────────────────────────────────
// TODO: read Jwt:Key, Jwt:Issuer, Jwt:Audience from appsettings.json
// TODO: builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer() with TokenValidationParameters

// ─────────────────────────────────────
// 10. Authorization Policies
// ─────────────────────────────────────
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Permissions.AdminPolicy,
        policy => policy.RequireRole("Administrator"));
    options.AddPolicy(Permissions.TenantUserPolicy,
        policy => policy.RequireRole("TenantUser"));
});

// ─────────────────────────────────────
// 11. log4net
// ─────────────────────────────────────
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly()!);
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// ─────────────────────────────────────
// 12. CORS
// ─────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ─────────────────────────────────────
// Build App
// ─────────────────────────────────────
var app = builder.Build();

// ─────────────────────────────────────
// Middleware Pipeline — ORDER MATTERS
// ─────────────────────────────────────
//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowFrontend");
//app.UseAuthentication();
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
        // log and continue — don't let seeder crash the app
        Console.WriteLine($"Seeder failed: {ex.Message}");
    }
}

app.Run();