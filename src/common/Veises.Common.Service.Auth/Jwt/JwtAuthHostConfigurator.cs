using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthHostConfigurator : IHostConfigurator
    {
        public Action<IApplicationBuilder> Configure()
        {
            return builder => { builder.UseAuthentication(); };
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
        {
            return (context, config) => { };
        }

        public Action<ServiceCollection> ConfigureServices()
        {
            return collection =>
            {
                collection.Services.AddSingleton<IAuthService, JwtAuthService>();
                collection.Services.AddSingleton<IJwtAuthConfigProvider, JwtAuthConfigProvider>();
                collection.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

                var jwtConfigProvider = new JwtAuthConfigProvider(collection.Configuration);

                var jwtConfig = jwtConfigProvider.GetConfig();

                collection
                    .Services
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