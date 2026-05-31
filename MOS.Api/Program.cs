// ─────────────────────────────────────
// Using Statements
// ─────────────────────────────────────
// TODO: add using statements as you implement each section

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────
// 1. Controllers + Filters
// ─────────────────────────────────────
// TODO: builder.Services.AddControllers(options =>
// {
//     options.Filters.Add<ValidationFilter>();
// });

// ─────────────────────────────────────
// 2. Swagger with JWT support
// ─────────────────────────────────────
// TODO: builder.Services.AddEndpointsApiExplorer();
// TODO: builder.Services.AddSwaggerGen() with JWT bearer security definition

// ─────────────────────────────────────
// 3. Database
// ─────────────────────────────────────
// TODO: builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(
//         builder.Configuration.GetConnectionString("DefaultConnection")));

// ─────────────────────────────────────
// 4. Repositories
// ─────────────────────────────────────
// TODO: builder.Services.AddScoped<IUserRepository, UserRepository>();
// TODO: builder.Services.AddScoped<ITenantRepository, TenantRepository>();
// TODO: builder.Services.AddScoped<IProductRepository, ProductRepository>();
// TODO: builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
// TODO: builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
// TODO: builder.Services.AddScoped<IAuditRepository, AuditRepository>();
// TODO: builder.Services.AddScoped<IEmailWhitelistRepository, EmailWhitelistRepository>();

// ─────────────────────────────────────
// 5. External Services
// ─────────────────────────────────────
// TODO: builder.Services.AddScoped<ITokenService, TokenService>();
// TODO: builder.Services.AddScoped<IPasswordService, PasswordService>();
// TODO: builder.Services.AddScoped<IEmailService, EmailService>();

// ─────────────────────────────────────
// 6. Application Services
// ─────────────────────────────────────
// TODO: builder.Services.AddScoped<AuthService>();
// TODO: builder.Services.AddScoped<UserService>();
// TODO: builder.Services.AddScoped<ProductService>();
// TODO: builder.Services.AddScoped<TenantService>();
// TODO: builder.Services.AddScoped<AuditService>();
// TODO: builder.Services.AddScoped<EmailWhitelistService>();

// ─────────────────────────────────────
// 7. Seeders
// ─────────────────────────────────────
// TODO: builder.Services.AddScoped<DatabaseSeeder>();
// TODO: builder.Services.AddScoped<ProductSeeder>();
// TODO: builder.Services.AddScoped<AdminSeeder>();
// TODO: builder.Services.AddScoped<EmailWhitelistSettingSeeder>();

// ─────────────────────────────────────
// 8. FluentValidation
// ─────────────────────────────────────
// TODO: builder.Services.AddFluentValidationAutoValidation();
// TODO: builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

// ─────────────────────────────────────
// 9. JWT Authentication
// ─────────────────────────────────────
// TODO: read Jwt:Key, Jwt:Issuer, Jwt:Audience from appsettings.json
// TODO: builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer() with TokenValidationParameters

// ─────────────────────────────────────
// 10. Authorization Policies
// ─────────────────────────────────────
// TODO: builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy(Permissions.AdminPolicy, 
//         policy => policy.RequireRole("Administrator"));
//     options.AddPolicy(Permissions.TenantUserPolicy, 
//         policy => policy.RequireRole("TenantUser"));
// });

// ─────────────────────────────────────
// 11. log4net
// ─────────────────────────────────────
// TODO: var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
// TODO: XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

// ─────────────────────────────────────
// 12. CORS
// ─────────────────────────────────────
// TODO: builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend", policy =>
//         policy.WithOrigins("http://localhost:3000")
//               .AllowAnyHeader()
//               .AllowAnyMethod());
// });

// ─────────────────────────────────────
// Build App
// ─────────────────────────────────────
var app = builder.Build();

// ─────────────────────────────────────
// Middleware Pipeline — ORDER MATTERS
// ─────────────────────────────────────
// TODO: app.UseMiddleware<ExceptionHandlingMiddleware>() ← always first
// TODO: app.UseMiddleware<RequestLoggingMiddleware>()
// TODO: app.UseSwagger()
// TODO: app.UseSwaggerUI()
// TODO: app.UseCors("AllowFrontend")
// TODO: app.UseAuthentication() ← must be before UseAuthorization
// TODO: app.UseAuthorization()
// TODO: app.MapControllers()

// ─────────────────────────────────────
// Run Seeders on Startup
// ─────────────────────────────────────
// TODO: using (var scope = app.Services.CreateScope())
// {
//     var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
//     await seeder.SeedAsync();
// }

app.Run();