using System;
using System.Data;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyProject.Persistence;
using Xunit;

namespace MyProject.UnitTests
{
    [Collection("Database collection")]
    public class TestBase : IDisposable
    {
        private readonly IDbContextTransaction transaction;

        protected TestBase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "unitTests",
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite(connection);

            Context = new AppDbContext(optionsBuilder.Options);
            Context.Database.AutoTransactionsEnabled = false;

            Context.Database.OpenConnection();
            try
            {
                Context.Database.EnsureCreated();
            }
            catch
            {
                // Already exists
            }

            transaction = Context.Database.BeginTransaction(IsolationLevel.Serializable);

            Mapper = TestFixture.Instance.Mapper;
        }

        protected AppDbContext Context { get; }
        protected IMapper Mapper { get; }

        public void Dispose()
        {
            transaction.Rollback();
        }
    }
}
