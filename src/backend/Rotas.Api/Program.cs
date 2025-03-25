
using Rotas.DataAccess.FileDataAccess.DependencyInjection;
using Rotas.Application.DependencyInjection;

namespace Rotas.Api;
public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        const string corsPolicyName = "frontEndUI";

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName,
                            policy =>
                            {
                                // policy.WithOrigins("http://localhost:*", "http://localhost:*/")
                                // http://localhost:4200/ ==> change to specific port
                                // .AllowAnyOrigin() ==> change to anywhere, any port
                                policy.SetIsOriginAllowed(origin => origin.Contains("://localhost"))
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                            });
        });
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        builder.Configuration
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.SetupUseCasesAndServicesFacade(); // helper to config all use cases and the façade Services
        builder.Services.SetupInfraFileDataAccess(builder.Configuration); // data access, repository


        var app = builder.Build();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(corsPolicyName); // tem que ficar ABAIXO do UseRouting()
        // app.UseAuthentication() ==> se hablitar deve ficar ABAIXO do Cors
        // app.UseAuthorization() ==> se hablitar deve ficar ABAIXO do Cors

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