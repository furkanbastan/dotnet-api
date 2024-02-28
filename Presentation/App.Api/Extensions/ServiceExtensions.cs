using App.Api.Models;
using App.Application;
using App.Infrastructure;
using App.Persistence;

namespace App.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCorsServices(this IServiceCollection services, IConfiguration configuration)
    {
        var policies = configuration.GetSection("CorsPolicies").Get<List<PolicyOption>>();

        if (policies is null || policies.Count == 0) return;

        services.AddCors(options =>
        {
            policies.ForEach(p =>
            {
                options.AddPolicy(p.Name!, policy => policy.WithOrigins(p.HttpOrigin!, p.HttpsOrigin!).AllowAnyHeader().AllowAnyMethod());
            });
        });
    }
    public static void AddLayerServices(this IServiceCollection services, string connectionString)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices();
        services.AddPersistenceServices(connectionString);
    }
}
