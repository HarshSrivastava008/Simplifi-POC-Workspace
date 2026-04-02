using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WrapSP.Application.Common.Interfaces;
using WrapSP.Infrastructure.Data;

namespace WrapSP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        return services;
    }
}
