using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Movies.Domain.RatingTypes;
using Serilog.Core;
using JsonWebToken;

namespace Movies.API
{
    public class RatingsExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<Logger>();

            if (context.Exception is Rating_NotFoundException)
            {
                var exception = context.Exception as Rating_NotFoundException;
                context.HttpContext.Response.StatusCode = 404;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            if (context.Exception is Rating_NoContentException)
            {
                var exception = context.Exception as Rating_NoContentException;
                context.HttpContext.Response.StatusCode = 204;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            if (context.Exception is Rating_InvalidOwnerException)
            {
                var exception = context.Exception as Rating_InvalidOwnerException;
                context.HttpContext.Response.StatusCode = 403;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            context.ExceptionHandled = true;
        }
    }
}
