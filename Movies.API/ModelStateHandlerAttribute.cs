using JsonWebToken;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API
{
    public class ModelStateHandlerAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = 400;
                var errors = context.ModelState.FieldErrorsToJson();
                var result = System.Text.Json.JsonSerializer.Serialize(errors);
                await context.HttpContext.Response.Body.WriteAsync(result.ToByteArray());
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}
