using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProject.API;

namespace MyProject.IntegrationTests
{
    /// <summary>
    ///     Test Startup should only be used for testing.
    ///     Override methods that should be different in testing.
    /// </summary>
    public class TestStartup : Startup
    {
        public TestStartup(IHostEnvironment env) : base(env)
        {
        }

        protected override void AddLogger(IServiceCollection services)
        {
            base.AddLogger(services);
        }

        protected override void AddMapper(IServiceCollection services)
        {
            base.AddMapper(services);
        }

        protected override void AddMediatR(IServiceCollection services)
        {
            base.AddMediatR(services);
        }

        protected override void AddOptions(IServiceCollection services)
        {
            base.AddOptions(services);
        }

        protected override void AddValidation(IServiceCollection services)
        {
            base.AddValidation(services);
        }
    }
}
