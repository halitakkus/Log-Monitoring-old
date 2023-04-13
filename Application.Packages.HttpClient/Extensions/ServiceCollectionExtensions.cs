using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.HttpClientService.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register in RabbitMQ dependencies to IServiceCollection
        /// </summary>
        /// <returns></returns>
        public static void AddHttpClientService(this IServiceCollection services)
        {

            services.AddSingleton<IHttpService, HttpService>();
        }
    }
}
