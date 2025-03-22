
using Rotas.DataAccess.FileDataAccess.DependencyInjection;
using Rotas.Application.DependencyInjection;
using Rotas.Domain.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appSettings.json");

builder.Services.SetupDomain();
builder.Services.SetupFileDataAccess(builder.Configuration); // data access, repository
builder.Services.SetupUseCasesAndServicesFacade(); // helper to config all use cases and the façade Services

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger")); // default redirect to Swagger on Dev env
}

app.MapControllers();
app.UseHttpsRedirection();

await app.RunAsync();
