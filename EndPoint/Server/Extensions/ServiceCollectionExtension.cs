using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace EndPoint.Server.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddServerLayer(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer")!;

        services.AddDataBase(connectionString);


        return services;
    }

    internal static IServiceCollection AddDataBase(this IServiceCollection service,string connection) => service
        .AddDbContext<DataBaseContext>(builder => builder.UseSqlServer(connection));
}