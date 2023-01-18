using Application.Email.Model;
using Application.MessageBroker.Bus;
using Application.MessageBroker.Events;
using Infra.Bus;
using Infra.Bus.Handler;
using Infra.SMTP;
using Microsoft.AspNetCore.Rewrite;
using static IdentityModel.OidcConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfigurationRoot configuration = new ConfigurationBuilder()
.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
.AddJsonFile("appsettings.json")
.Build();
builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
builder.Services.ConfigureSMTPService();
builder.Services.ConfigureBusService();
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", o => {
    o.Authority = "http://localhost:8077";
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() { ValidateAudience = false };
    o.RequireHttpsMetadata = false;
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e => e.MapDefaultControllerRoute().RequireAuthorization());
app.MapControllers();
var scope = app.Services.CreateScope();
var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
eventBus.Subscribe<UserCreatedEvent, UserCreatedHandler>();//use task.run
app.Run();
