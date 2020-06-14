using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonWebToken;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movies.Domain.ReviewTypes;
using Serilog.Core;

namespace Movies.API
{
    public class ReviewsExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<Logger>();

            if (context.Exception is Review_NotFoundException)
            {
                var exception = context.Exception as Review_NotFoundException;
                context.HttpContext.Response.StatusCode = 404;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            if (context.Exception is Review_NoContentException)
            {
                var exception = context.Exception as Review_NoContentException;
                context.HttpContext.Response.StatusCode = 204;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            if (context.Exception is Review_InvalidOwnerException)
            {
                var exception = context.Exception as Review_InvalidOwnerException;
                context.HttpContext.Response.StatusCode = 403;
                await context.HttpContext.Response.Body.WriteAsync(exception.Message.ToByteArray());
                logger.Debug(exception.Message);
            }

            context.ExceptionHandled = true;
        }
    }
}
