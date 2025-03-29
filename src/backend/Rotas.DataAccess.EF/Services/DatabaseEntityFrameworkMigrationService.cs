
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rotas.DataAccess.EF.Data;

namespace Rotas.DataAccess.EF.Services;

public static class DatabaseEntityFrameworkMigrationService
{
    public static void ExecuteMigrations(this IServiceProvider serviceProvider, ILogger logger)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RotasDbContext>();
        var retries = 5;
        var delay = TimeSpan.FromSeconds(5);

        for (var i = 0; i < retries; i++)
        {
            try
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
                return;
            }
            catch (MySqlConnector.MySqlException mEx)
            {
                if (!mEx.Message.Trim().ToLowerInvariant().Contains("unable to connect to any of the specified mysql hosts"))
                    throw;

                if (i == retries - 1) throw;
                logger.Warning($"Database connection failed. Retrying in {delay.Seconds} seconds...");
                Thread.Sleep(delay);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
