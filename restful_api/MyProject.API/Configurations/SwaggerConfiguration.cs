using System;
using System.IO;
using System.Reflection;
using MyProject.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MyProject.API.Configurations
{
    /// <summary>
    ///     Configurations for Swagger gen and UI
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        ///     Add SwaggerGen and UI to services
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyProject",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);

                options.CustomSchemaIds(type => type.NestedClassName());

                options.DescribeAllParametersInCamelCase();
            });

            return services;
        }

        /// <summary>
        ///     Use swagger to setup endpoint for swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProject v1");
                options.RoutePrefix = "api";

                options.DocExpansion(DocExpansion.None);

                options.DisplayRequestDuration();
            });

            return app;
        }
    }
}
