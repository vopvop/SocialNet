using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Veises.Common.Service.Auth.Jwt;

namespace Veises.Common.Service.Auth
{
    internal sealed class JwtAuthHostConfigurator: IHostConfigurator
    {
		private IConfiguration _configuration;

		public Action<IApplicationBuilder> Configure()
		{
			return builder =>
			{
				builder.UseAuthentication();

				_configuration = builder.ApplicationServices.GetRequiredService<IConfiguration>();
			};
		}

		public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, config) => { };

		public Action<IServiceCollection> ConfigureServices()
		{
			return services =>
			{
				services.AddSingleton<IAuthService, JwtAuthService>();
				services.AddSingleton<IJwtAuthConfigProvider, JwtAuthConfigProvider>();
				services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

				var jwtConfigProvider = new JwtAuthConfigProvider(_configuration);

				var jwtConfig = jwtConfigProvider.GetConfig();

				services
					.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(
						options => options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = jwtConfig.Issuer,
							ValidAudience = jwtConfig.Audience,
							IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.Key)
						});
			};
		}
	}
}