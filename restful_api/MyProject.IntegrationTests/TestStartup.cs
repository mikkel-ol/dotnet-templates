using MyProject.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

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
    }
}
