using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Movies.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Persistency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistency(this IServiceCollection services)
        {
            services.AddDbContext<MoviesDbContext>(options =>
            {
                options.UseSqlite("Filename=../Movies.Persistency/Movies.db", opt =>
                {
                    opt.MigrationsAssembly(
                        typeof(MoviesDbContext).Assembly.FullName
                    );
                });

                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IApplicationDbContext>(ctx =>
                ctx.GetRequiredService<MoviesDbContext>());

            return services;
        }
    }
}
