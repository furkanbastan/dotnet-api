using App.Application.Abstractions.Repositories;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppEfContext>(options => options.UseSqlite(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //var seed = new SeedData();
        //seed.SeedAsync(connectionString).GetAwaiter();
    }
}
