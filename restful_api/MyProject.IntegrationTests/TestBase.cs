using System;
using System.Data;
using System.Net.Http;
using MediatR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using MyProject.Persistence;

namespace MyProject.IntegrationTests
{
    [Collection("Test collection")]
    public abstract class TestBase : IDisposable
    {
        private readonly IDbContextTransaction transaction;

        protected TestBase(TestFixture testFixture)
        {
            Client = testFixture.Client;
            Server = testFixture.Server;

            Context = ServiceProvider.GetService<AppDbContext>();

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
        }

        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        protected AppDbContext Context { get; }
        private IServiceProvider ServiceProvider => Server.Services;
        protected IMediator Mediator => ServiceProvider.GetService<IMediator>();

        public void Dispose()
        {
            transaction.Rollback();
        }
    }
}
