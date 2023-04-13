using Application.DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test.Core
{
    public class TestWebApplicationFactory<TStartup> : IDisposable where TStartup : class
    {
        private readonly string _hostingEnvironment = "Integration";

        private WebApplicationFactory<TStartup>? _webApplicationFactory;
        public WebApplicationFactory<TStartup> WebApplicationFactory
        {
            get
            {
                return _webApplicationFactory ?? this.ConfigureWebApplicationFactory();
            }
        }
        public void ConfigureTestOptions()
        {
            //Action<string> configureDatabase = (databaseName) =>
            //{
            //    this.DatabaseFixture = new DatabaseFixture();
            //};

            Action<Action<IServiceCollection>> configureWebHost = (Action<IServiceCollection> configureTestServices) =>
            {
                _webApplicationFactory = this.ConfigureWebApplicationFactory(configureTestServices);
            };
        }
        private WebApplicationFactory<TStartup> ConfigureWebApplicationFactory(Action<IServiceCollection> configureTestServices = null)
        {
            return new WebApplicationFactory<TStartup>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment(_hostingEnvironment);

                // Before TStartup ConfigureServices.
                builder.ConfigureServices(collection =>
                {
                    collection.AddDbContext<ApplicationDbContext>(options =>
                    {

                    });
                });

                // After TStartup ConfigureServices.
                builder.ConfigureTestServices(collection =>
                {
                    configureTestServices?.Invoke(collection);
                });
            });
        }

        public void Dispose()
        {
            if(this.WebApplicationFactory != null)
                this.WebApplicationFactory.Dispose();
        }
    }
}
