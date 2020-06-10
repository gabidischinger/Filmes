using Microsoft.AspNetCore.Mvc.Filters;
using Movies.Application;
using Movies.Domain;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Movies.API
{
    public class RestrictReviewAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetService<Logger>();
            var handler = context.HttpContext.RequestServices.GetService<IEntityCrudHandler<Movie>>();
            var routeData = context.RouteData.Values.Keys.Select(
                    key => $"{key} : {context.RouteData.Values[key]}"
                );
            var userID = (int)context.RouteData.Values["UserID"];
            var model = context.ActionArguments;


            if (model.ContainsKey("review"))
            {
                Review review = model["review"] as Review;
                Movie movie = await handler.ObterUm(review.MovieID, userID);
                if (movie != null)
                {
                    await next.Invoke();
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 403;
                    await context.HttpContext.Response.WriteAsync("Seu usuário não tem permissão para usar esse recurso");
                    logger.Warning("Acesso Proibido do usuário {userID} ao recurso {@routeData} | Model : {@model}",
                        userID, routeData, model);
                }
            }
        }
    }
}
