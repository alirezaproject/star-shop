using Application.Interfaces.Contexts;
using Application.Interfaces.Services.Categories;
using Application.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace EndPoint.Server.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddServerLayer(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer")!;

        services.AddDataBase(connectionString);
        services.AddApplicationServices();

        return services;
    }

    internal static IServiceCollection AddDataBase(this IServiceCollection service,string connection) => service
        .AddDbContext<DataBaseContext>(builder => builder.UseSqlServer(connection));

    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<IDataBaseContext, DataBaseContext>();
        services.AddScoped<ICategoryTypeService, CategoryTypeService>();

        return services;
    }
}