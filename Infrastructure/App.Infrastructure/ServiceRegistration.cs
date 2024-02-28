using App.Application.Abstractions.Services;
using App.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
