using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Veises.Common.Service
{
    public interface IHostConfigurator
    {
		Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp();

		Action<IServiceCollection> ConfigureServices();

		Action<IApplicationBuilder> Configure();
    }
}