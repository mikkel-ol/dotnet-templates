using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyProject.API.Configurations;
using MyProject.API.Filters;

namespace MyProject.API
{
    public class Startup
    {
        protected readonly IHostEnvironment env;
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            this.env = env;

            // When running tests, reload of configuration json causes concurrency problems
            var shouldReloadConfigs = !env.EnvironmentName.Equals("Test");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, shouldReloadConfigs)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, shouldReloadConfigs)
                .AddJsonFile("appsettings.Local.json", true, true)
                .AddEnvironmentVariables("MyProject_");

            try
            {
                if (env.IsDevelopment()) builder.AddUserSecrets<Startup>();
            }
            catch (InvalidOperationException)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Missing user secrets initialization");
                Console.ResetColor();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddOptions(services);

            AddMediatR(services);

            AddLogger(services);

            AddMapper(services);

            AddValidation(services);

            services.AddSwagger();

            services.AddSingleton(_ => Configuration);

            services.AddApplicationInsightsTelemetry();

            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseForwardedHeaders();

            app.UseRouting();

            app.UseSwagger();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        protected virtual void AddLogger(IServiceCollection services)
        {
            services.AddLogger(Configuration);
        }

        protected virtual void AddMapper(IServiceCollection services)
        {
            services.AddMapper(Configuration);
        }

        protected virtual void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(Configuration);
        }

        protected virtual void AddValidation(IServiceCollection services)
        {
            services.AddValidation(Configuration);
        }

        protected virtual void AddOptions(IServiceCollection services)
        {
            // Add custom option objects here
        }
    }
}
