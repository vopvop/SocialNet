﻿using Veises.Common.Service;
using Veises.Common.Service.Versionning;
using Veises.Common.Services.Swagger;

namespace Veises.Service.Example
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly)
                .WithDefaultConfigFile()
                .WithLogging()
				.WithApiVersionning()
				.WithSwagger("Example REST service", "An example REST API service on Kestrel with API versioning support")
                .Build())
            {
                serviceHost.Run();
            }
        }
    }
}