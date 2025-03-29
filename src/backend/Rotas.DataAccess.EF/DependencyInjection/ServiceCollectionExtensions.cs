using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotas.DataAccess.EF.Data;
using Rotas.Domain.Entities;
using Rotas.Domain.Interfaces;

namespace Rotas.DataAccess.EF.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupDataAccessEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MysqlDB");
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException("Connection string 'MysqlDB' not found in appsettings.json");

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<RotasDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        services.AddScoped<IRepositoryCrud<Deslocamento>, RepositoryCrud>();

        return services;
    }



}
