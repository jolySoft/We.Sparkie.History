using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using We.Sparkie.History.Api;

namespace We.Sparkie.History.Tests
{
    public class ComponentTestBase
    {
        private MongoDbRunner _runner;
        protected TestServer TestServer { get; private set; }
        protected HttpClient Client { get; private set; }
        protected IMongoDatabase Database { get; private set; }

        public ComponentTestBase()
        {
            _runner = MongoDbRunner.StartForDebugging();
            var client = new MongoClient(_runner.ConnectionString);
            Database = client.GetDatabase("Sparkie");

            Startup.OnConfigureService = services =>
            {
                services.AddScoped<IMongoDatabase>((_) => Database);
            };

            TestServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = TestServer.CreateClient();
        }
    }
}