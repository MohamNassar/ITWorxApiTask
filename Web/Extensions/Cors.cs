using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Web.Contracts.Cors;

namespace Web.Extensions
{
    public static class Cors
    {
        private const string CorsAllowSpecific = "CorsAllowSpecific";
        private const string CorsAllowAll = "CorsAllowAll";
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsSettings corsAllowAll = configuration.GetCorsSettings();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsAllowAll,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                if (corsAllowAll.HasValue)
                {

                    options.AddPolicy(CorsAllowSpecific,
                        p => p.WithOrigins(corsAllowAll.Origins).WithHeaders(corsAllowAll.Headers)
                                        .WithMethods(corsAllowAll.Methods)
                                        .SetPreflightMaxAge(new TimeSpan(corsAllowAll.PreflightMaxAge.Value))
                                        );

                }

            });
        }

   
        public static void UseCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            CorsSettings corsAllowAll = configuration.GetCorsSettings();
            app.UseCors(corsAllowAll.HasValue ? CorsAllowSpecific : CorsAllowAll);
        }

        private static CorsSettings GetCorsSettings(this IConfiguration configuration)
        {
            var result = new CorsSettings();
            configuration.GetSection("CorsSettings").Bind(result);
            return result;
        }
    }
}
