using Application;
using Infra.Persistance;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(p => p.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "BackendApi Documentation" }));
builder.Services.ConfigurePersistanceService();
builder.Services.ConfigureApplicationService();
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", o => { o.Authority = "http://localhost:8077"; 
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
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(e => e.MapDefaultControllerRoute().RequireAuthorization());
app.MapControllers();

app.Run();
