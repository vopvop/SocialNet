using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

using Swashbuckle.AspNetCore.Swagger;

namespace Veises.SocialNet.Identity
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvcCore()
				.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

			services
				.AddMvc(options =>
				{
					options.RespectBrowserAcceptHeader = true;
				})
				.AddXmlSerializerFormatters()
				.AddXmlDataContractSerializerFormatters();

			services.AddApiVersioning(c =>
			{
				c.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
				c.ReportApiVersions = true;
			});

			services.AddSwaggerGen(c =>
			{
				var provider = services
					.BuildServiceProvider()
					.GetRequiredService<IApiVersionDescriptionProvider>();

				foreach (var description in provider.ApiVersionDescriptions)
				{
					c.SwaggerDoc(
						description.GroupName,
						new Info()
						{
							Title = $"Veises User Identity API {description.ApiVersion}",
							Description = "User identity API service",
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
				var xmlPath = Path.Combine(basePath, "Veises.SocialNet.Identity.xml");
				c.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			IApiVersionDescriptionProvider provider)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				loggerFactory.AddDebug(LogLevel.Debug);
			}

			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				foreach (var description in provider.ApiVersionDescriptions)
				{
					c.SwaggerEndpoint(
						$"/swagger/{description.GroupName}/swagger.json",
						description.GroupName.ToUpperInvariant());
				}
			});
		}
	}
}