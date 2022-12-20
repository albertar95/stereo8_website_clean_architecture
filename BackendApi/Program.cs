using Application;
using Infra.Persistance;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(p => p.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "BackendApi Documentation" }));
builder.Services.ConfigurePersistanceService();
builder.Services.ConfigureApplicationService();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
