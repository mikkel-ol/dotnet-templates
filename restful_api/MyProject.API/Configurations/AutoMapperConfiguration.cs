using AutoMapper;
using MyProject.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyProject.API.Configurations
{
    /// <summary>
    ///     Extension method for IServiceCollection
    /// </summary>
    public static class AutoMapperConfiguration
    {
        /// <summary>
        ///     Adds AutoMapper
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection">services</see> is used to access the service collection</param>
        /// <param name="configuration">Application configuration object</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AssemblyAnchor).Assembly, typeof(Startup).Assembly);

            return services;
        }
    }
}
