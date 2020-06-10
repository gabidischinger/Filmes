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
using System.Text.RegularExpressions;

namespace JsonWebToken
{
    class RestrictRatingAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetService<Logger>();
            var handler = context.HttpContext.RequestServices.GetService<IEntityCrudHandler<Rating>>();
            var routeData = context.RouteData.Values.Keys.Select(
                    key => $"{key} : {context.RouteData.Values[key]}"
                );


            var req = context.HttpContext.Request;
            var path = req.Path.ToString();

            var pattern = @"/([=\w_-]+)/([=\w_-]+)/([=\w_-]+)";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match match = regex.Match(path);

            int ratingId = int.Parse(match.Groups[3].Value);


            var userID = (int)context.RouteData.Values["UserID"];
            var model = context.ActionArguments;


            var rating = await handler.ObterUm(ratingId, userID);
            if (rating != null)
            {
                await next.Invoke();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 403;
                await context.HttpContext.Response.WriteAsync("Seu usuário não tem permissão para fazer essa alteração");
                logger.Warning("Acesso Proibido do usuário {userID} ao recurso {@routeData} | Model : {@model}",
                    userID, routeData, model);
            }

            //if (model.ContainsKey("Rating"))
            //{
                
            //}
        }
    }
}
