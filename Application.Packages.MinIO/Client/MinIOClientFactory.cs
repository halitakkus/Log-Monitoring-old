using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.MinIO.Configuration;
using Minio;

namespace Application.Packages.MinIO.Client
{
    class MinIOClientFactory : IMinIOClientFactory
    {
        public MinioClient Client { get; }

        public MinIOClientFactory(IMinIOConfiguration configuration)
        {
            Client = new MinioClient(configuration.EndPoint, configuration.AccessKey, configuration.SecretKey);
        }
    }
}
