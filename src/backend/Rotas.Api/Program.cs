

using Rotas.Application.DependencyInjection;
using Rotas.DataAccess.EF.DependencyInjection;
using Rotas.DataAccess.EF.Data;
using Serilog;
using Rotas.DataAccess.EF.Services;

namespace Rotas.Api;
public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        const string corsPolicyName = "frontEndUI";
        Log.Logger = GetLoggerConfiguration();
        Log.Information("Log created...");
        string? connectionString = string.Empty;
        try
        {
            Log.Information("Starting setup ...");
            Log.Information("WebApplication.CreateBuidler ...");
            var builder = WebApplication.CreateBuilder(args);
            if (builder == null)
                throw new ArgumentNullException(nameof(builder), "WebApplicationBuilder is null");

            builder.Configuration.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
            connectionString = builder.Configuration.GetConnectionString("MysqlDB");
            SetupCors(builder.Services, corsPolicyName);
            builder.Services.AddSerilog();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();
            builder.Services.AddControllers();

            builder.Services.SetupUseCasesAndServicesFacade(); // helper to config all use cases and the façade Services
            builder.Services.SetupDataAccessEntityFramework(builder.Configuration);

            Log.Information("app = builder.Build() ...");
            var app = builder.Build();

            Log.Information("app.Services.ExecuteMigrations() ...");
            app.Services.ExecuteMigrations(Log.Logger); // execute EF migrations on startup
            app.UseSerilogRequestLogging(); // log all requests and responses
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(corsPolicyName); // tem que ficar ABAIXO do UseRouting(), SEMPRE
            SetupWhenIsDevelopment(app);
            app.MapControllers();
            app.UseHttpsRedirection();

            await app.RunAsync();
        }
        catch (System.Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.Information("Application is shutting down...");
            Log.Information("Connection string: [{connectionString}]", connectionString);
            Log.CloseAndFlush();
        }
    }//..main


    private static void SetupWhenIsDevelopment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapGet("/", () => Results.Redirect("/swagger")); // default redirect to Swagger on Dev env
        }
    }

    private static Serilog.Core.Logger GetLoggerConfiguration() =>
            new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


    private static void SetupCors(IServiceCollection services, string corsPolicyName)
    {
        services.AddSingleton(Log.Logger);
        services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName,
                            policy =>
                            {
                                policy
                                // app.UseAuthentication() ==> se hablitar deve ficar ABAIXO do Cors
                                // app.UseAuthorization() ==> se hablitar deve ficar ABAIXO do Cors
                                .AllowAnyOrigin() // ==> anywhere, any port
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                            });
        });
    }



}//..class
