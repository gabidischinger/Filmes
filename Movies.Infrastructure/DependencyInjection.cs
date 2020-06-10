using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;

namespace Movies.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSerilog(this IServiceCollection services)
        {
            services.AddScoped<Logger>((provider) =>
            new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .WriteTo.File(
                    "LOG_.txt",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                ).CreateLogger()
            );

            return services;
        }
    }
}
