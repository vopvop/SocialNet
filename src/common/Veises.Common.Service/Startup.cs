using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Veises.Common.Service.Auth;
using Veises.Common.Service.Auth.Jwt;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service
{
    internal sealed class Startup
    {
        private readonly AssemblyProvider _assemblyProvider;

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, AssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider ?? throw new ArgumentNullException(nameof(assemblyProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app
                .UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
            services.AddSingleton<IAuthService, JwtAuthService>();
            services.AddSingleton<IJwtAuthConfigProvider, JwtAuthConfigProvider>();
            services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

            var mvcBuilder = services
                .AddMvcCore(options => options.RespectBrowserAcceptHeader = true)
                .AddJsonFormatters()
                .AddXmlSerializerFormatters();

            foreach (var assembly in _assemblyProvider.GetAssemblies())
            {
                mvcBuilder.AddApplicationPart(assembly);
            }

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
        }
    }
}