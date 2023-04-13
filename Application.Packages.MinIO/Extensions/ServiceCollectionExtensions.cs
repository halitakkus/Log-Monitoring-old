using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.MinIO.Client;
using Application.Packages.MinIO.Configuration;
using Application.Packages.MinIO.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Packages.MinIO.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions contains extended IServiceCollection's methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register MinIO package dependencies.
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddMinIO(this IServiceCollection services, Action<IMinIOConfiguration> configuration)
        {
            IMinIOConfiguration minIoConfiguration = new MinIOConfiguration();
            configuration.Invoke(minIoConfiguration);

            services.AddSingleton<IMinIOConfiguration>(minIoConfiguration);
            
            services.AddSingleton<IMinIOClientFactory, MinIOClientFactory>();

            services.AddSingleton<IMinIOService, MinIOService>();

            return services;
        }
    }
}
