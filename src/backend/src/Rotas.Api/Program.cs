
using Rotas.Application.UseCases.Viagens;
using Rotas.Domain.Services;
using Rotas.DataAccess.FileDataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appSettings.json");

builder.Services.SetupFileDataAccess(builder.Configuration); // registering data access
builder.Services.AddScoped<CadastroViagemService>(); // registering services, need for Use Cases
builder.Services.AddScoped<AddViagemUseCase>(); // Registering use cases

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
