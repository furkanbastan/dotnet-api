using App.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)).ToArray();

            var errorList = new List<ValidationErrorModel>();

            foreach (var error in errorsInModelState)
            {
                foreach (var item in error.Value!)
                {
                    errorList.Add(new ValidationErrorModel { FieldName = error.Key, Message = item });
                }
            }

            context.Result = new BadRequestObjectResult(new
            {
                errors = errorList
            });
            return; // this is short-circuit
        }
        await next();
    }
}
