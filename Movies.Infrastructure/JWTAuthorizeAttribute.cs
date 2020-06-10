using JsonWebToken;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure
{
    public class JWTAuthorizeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Items.ContainsKey("JWTPayload"))
            {
                var payload = (JWTPayload)context.HttpContext.Items["JWTPayload"];
                var uid = int.Parse(payload.uid);
                var role = payload.role;
                var controller = (ControllerBase)context.Controller;
                controller.RouteData.Values["UserID"] = uid;
                controller.RouteData.Values["Role"] = role;
                await next.Invoke();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.Body.WriteAsync("Obtenha um token em /api/Authorization/GenerateToken".ToByteArray());
            }
        }
    }
}
