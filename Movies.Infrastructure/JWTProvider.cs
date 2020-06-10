using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Infrastructure
{
    public static class JWTProvider
    {
        public static IApplicationBuilder UseJWTAuthorization(this IApplicationBuilder application)
        {
            application.Map("/api/Authorization/GenerateToken", app => {
                app.UseMiddleware<GenerateTokenMiddleware>();
            });

            application.UseMiddleware<ValidateTokenMiddleware>();

            return application;
        }
    }
}
