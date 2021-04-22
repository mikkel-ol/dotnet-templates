using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Application;

namespace MyProject.API.Configurations
{
    /// <summary>
    ///     Extension method for IServiceCollection
    /// </summary>
    public static class ValidationConfiguration
    {
        /// <summary>
        ///     Adds validation
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection">services</see> is used to access the service collection</param>
        /// <param name="configuration">Application configuration object</param>
        /// <returns>The service collection</returns>
        public static IServiceCollection AddValidation(this IServiceCollection services, IConfiguration configuration)
        {
            var validators = AssemblyScanner.FindValidatorsInAssemblies(new[] {typeof(AssemblyAnchor).Assembly});
            validators.ForEach(validator => services.AddTransient(validator.InterfaceType, validator.ValidatorType));

            return services;
        }
    }
}
