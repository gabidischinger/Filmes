using JsonWebToken;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Movies.Domain.MovieTypes;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Movies.API
{
    public class MoviesExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<Logger>();

            if (context.Exception is Movie_NotFoundException)
            {
                var exception = context.Exception as Movie_NotFoundException;
                context.HttpContext.Response.StatusCode = 404;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            if (context.Exception is Movie_NoContentException)
            {
                var exception = context.Exception as Movie_NoContentException;
                context.HttpContext.Response.StatusCode = 204;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            context.ExceptionHandled = true;
        }
    }
}
