
using Microsoft.AspNetCore.Mvc;
using Rotas.Application.Dtos;
using Rotas.Application.UseCases.Viagens;
using Rotas.Domain.Services;

// setup default start route to redirect to swagger


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// registering services
builder.Services.AddScoped<CadastroViagemService>();

// Registering use cases
builder.Services.AddScoped<AddViagemUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapControllers();
app.UseHttpsRedirection();



await app.RunAsync();
