using System.Net;
using System.Net.Mime;
using System.Text.Json;
using App.Api.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace App.Api.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        var policies = configuration.GetSection("CorsPolicies").Get<List<PolicyOption>>();

        if (policies is null || policies.Count == 0) return;

        policies.ForEach(p =>
        {
            app.UseCors(p.Name!);
        });
    }

    // Global exception handler middleware extension !!
    public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.Use(async (HttpContext context, Func<Task> next) =>
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception error)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var features = context.Features.Get<IExceptionHandlerFeature>();

                if (features is not null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = 400,
                        error.Message
                    }));
                }
            }
        });
    }
}
