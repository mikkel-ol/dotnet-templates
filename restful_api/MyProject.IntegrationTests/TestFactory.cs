using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using MyProject.API;
using Serilog;

namespace MyProject.IntegrationTests
{
    /// <summary>
    ///     TestFactory to setup WebApplicationFactory, eg. using TestStartup as entry point for the test server.
    ///     The generic parameter of WebApplicationFactory is where the content root of the test server should start,
    ///     could be foo, bar, Startup ect.
    /// </summary>
    /// <typeparam name="TEntry">The entry point for the test server</typeparam>
    public class TestFactory<TEntry> : WebApplicationFactory<Startup> where TEntry : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseEnvironment("Test")
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<TEntry>(); });
        }
    }
}
