using Application.Business.Abstract;
using Application.Business.Concrete;
using Application.Core.AspectOrientedProgramming.InterceptModule;
using Application.Core.Configuration.Context;
using Application.DataAccess.Abstract.Profile;
using Application.DataAccess.Extensions;
using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Business.Extensions
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
        public static IServiceCollection AddBusinessModule(this IServiceCollection services, IApplicationConfigurationContext configurationContext)
        {
            //Inject Fluent Validation Rule
            services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

            //Inject AutoMapper
            services.AddAutoMapper(typeof(ProfileBase));

            // Register data access module. 🎉
            services.AddDataAccessModule(configurationContext);

            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<ISettingManager, SettingManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
        public static void AddBusinessModule(this ContainerBuilder builder)
        {
            var interceptorModule = new AutofacInterceptorModule();
            interceptorModule.Load(typeof(ServiceCollectionExtensions).Assembly);
            builder.RegisterModule(interceptorModule);
        }
    }
}
