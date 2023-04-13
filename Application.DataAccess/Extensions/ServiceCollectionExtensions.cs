using Application.Core.Configuration.Context;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Services.Api;
using Application.DataAccess.Services.HttpClientService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DataAccess.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register business module dependencies to IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddDataAccessModule(this IServiceCollection services, IApplicationConfigurationContext configurationContext)
        {
            services.AddDbContext<ApplicationDbContext>(opt=> opt.UseNpgsql(configurationContext.ConnectionString) ,ServiceLifetime.Singleton);
            
            services.AddDal();
        }

        public static void AddDal(this IServiceCollection services)
        {
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IUserService, UserSevice>();

            services.AddSingleton<IRemoteWorkDal, RemoteWorkDal>();
            services.AddSingleton<ISettingDal, SettingDal>();
        }
    }
}
