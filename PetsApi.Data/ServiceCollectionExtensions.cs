using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PetsApi.Data;

public static class ServiceCollectionExtensions
{
    public static void AddPetsDbContext(this IServiceCollection services)
    {
        services.AddDbContext<PetsDbContext>(
            options => options.UsePetsSqlLite());
    }

    public static void UsePetsSqlLite(this DbContextOptionsBuilder options)
    {
        options.UseSqlite("Datasource=petsapi.db");
    }
}