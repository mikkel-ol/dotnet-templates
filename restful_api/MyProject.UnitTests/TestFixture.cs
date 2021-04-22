using System;
using AutoMapper;
using MyProject.API;
using MyProject.Application;
using Microsoft.Data.Sqlite;
using Xunit;

namespace MyProject.UnitTests
{
    /// <summary>
    ///     Test Fixture for sharing the singleton across tests
    /// </summary>
    public class TestFixture : IDisposable
    {
        private static TestFixture instance;
        private static readonly object Padlock = new object();

        public TestFixture()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "unitTests",
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };
            var connectionString = connectionStringBuilder.ToString();
            Connection = new SqliteConnection(connectionString);

            var mappingConfiguration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(AssemblyAnchor).Assembly, typeof(Startup).Assembly));
            Mapper = new Mapper(mappingConfiguration);
        }

        public Mapper Mapper { get; }
        private SqliteConnection Connection { get; }

        public static TestFixture Instance
        {
            get
            {
                if (instance != null) return instance;

                lock (Padlock)
                {
                    if (instance == null) instance = new TestFixture();
                }

                return instance;
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
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
