using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Xunit;

namespace MyProject.IntegrationTests
{
    /// <summary>
    ///     Test Fixture for sharing the singleton across tests
    /// </summary>
    public sealed class TestFixture : IDisposable
    {
        public TestFixture()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "integrationTests",
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };
            var connectionString = connectionStringBuilder.ToString();
            Connection = new SqliteConnection(connectionString);

            var factory = new TestFactory<TestStartup>().WithWebHostBuilder(options => { });

            Client = factory.CreateClient();

            // Server is first created after call to CreateClient
            Server = factory.Server;
        }

        public HttpClient Client { get; }
        public TestServer Server { get; }
        private SqliteConnection Connection { get; }

        public void Dispose()
        {
            Connection.Dispose();
            Server.Dispose();
            Client.Dispose();
        }
    }

    [CollectionDefinition("Test collection")]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
