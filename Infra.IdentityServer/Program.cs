using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerHost.Quickstart.UI;
using Infra.IdentityServer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
var MigrationAsembly = typeof(Program).Assembly.GetName().Name;
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(TestUsers.Users)
    .AddConfigurationStore(o => { o.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("IdentityDb"), sql => sql.MigrationsAssembly(MigrationAsembly)); }) //used for configuration data such as clients, resources, and scopes
    .AddOperationalStore(o => { o.ConfigureDbContext = b => b.UseSqlServer(configuration.GetConnectionString("IdentityDb"), sql => sql.MigrationsAssembly(MigrationAsembly)); }); //used for temporary operational data such as authorization codes, and refresh tokens
builder.Services.AddDbContext<ConfigurationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("IdentityDb")));
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()
    .AddOpenIdConnect("oidc", "Audioshop Identity Server", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;
        options.Authority = "http://localhost:8077/";
        options.ClientId = "interactive.confidential";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    DbTransfer.Put(dbContext);
}
app.MapGet("/", () => "welcome to audioshop identity server!");
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
