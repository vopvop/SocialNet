using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Veises.Common.Service.Swagger
{
    internal sealed class SwaggerHostConfigurator : IHostConfigurator
    {
        private readonly string _title;

        private readonly string _description;

        public SwaggerHostConfigurator(string title, string description)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public Action<IApplicationBuilder> Configure() => builder =>
        {
            builder.UseStaticFiles();

            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                var provider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        };

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, config) => { };

        public Action<ServiceCollection> ConfigureServices() => collection =>
        {
            collection.Services.AddMvc();

            collection.Services.AddSwaggerGen(c =>
            {
                var provider = collection
                    .Services
                    .BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(
                        description.GroupName,
                        new Info()
                        {
                            Title = $"{_title} {description.ApiVersion}",
                            Description = _description,
                            Version = description.ApiVersion.ToString(),
                            Contact = new Contact
                            {
                                Name = "Maksim Sharonov",
                                Url = "https://github.com/msharonov"
                            },
                            License = new License
                            {
                                Name = "GPL-3.0",
                                Url = "https://raw.githubusercontent.com/msharonov/Veises.SocialNet/master/LICENSE"
                            }
                        });
                }

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var resultFile = PlatformServices.Default.Application.ApplicationName;
                var xmlPath = Path.Combine(basePath, $"{resultFile}.xml");

                c.IncludeXmlComments(xmlPath);
            });
        };
    }
}