
using Rotas.DataAccess.FileDataAccess.DependencyInjection;
using Rotas.Application.DependencyInjection;

namespace Rotas.Api;
public class Program
{
    protected Program() { }
    
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        builder.Configuration
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.SetupUseCasesAndServicesFacade(); // helper to config all use cases and the façade Services
        builder.Services.SetupInfraFileDataAccess(builder.Configuration); // data access, repository


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
    }
}