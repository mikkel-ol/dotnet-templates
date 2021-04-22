using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Application;
using MyProject.Infrastructure.Pipelines;

namespace MyProject.API.Configurations
{
    /// <summary>
    ///     Extension method for IServiceCollection
    /// </summary>
    public static class MediatRConfiguration
    {
        /// <summary>
        ///     Adds MediatR
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection">services</see> is used to access the service collection</param>
        /// <param name="configuration">Application configuration object</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection AddMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationMediatrPipeline<,>));

            services.AddMediatR(typeof(AssemblyAnchor).GetTypeInfo().Assembly);

            return services;
        }
    }
}
