using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using JetBrains.Annotations;

namespace Veises.Common.Service
{
    public interface IHostConfigurator
    {
        [NotNull]
        Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp();

        [NotNull]
        Action<ServiceCollection> ConfigureServices();

        [NotNull]
        Action<IApplicationBuilder> Configure();
    }
}