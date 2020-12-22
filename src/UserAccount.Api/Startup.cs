using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using UserAccount.Infrastructure;
using UserAccount.Infrastructure.Cache;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

namespace UserAccount.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
         private const int CacheMemorySize = 350000000;

        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck("Default", () => HealthCheckResult.Healthy("OK"))
            ;

            var options = Options.Create(new DbClientOptions
            {
                LittleBigPlanetData = () => new SqlConnection(_configuration.GetConnectionString("LittleBigPlanetData")),
            });
            services.AddTransient(x => options);

            CacheConfiguration cache = new CacheConfiguration();
            var cacheSection = _configuration.GetSection("CacheConfiguration");
            cacheSection.Bind(cache);
            services.AddMemoryCache(cacheOptions =>
            {
                cacheOptions.SizeLimit = long.TryParse(cacheSection["CacheMemorySize"], out var result) ? result : CacheMemorySize;
            });
            services.AddTransient(x => cache);

            // ProviderConfigurationDictionary configurations = new ProviderConfigurationDictionary();
            // configurations.ProviderConfigurations = GetProvidersConfig();
            // services.AddTransient(x => configurations);

            services
               .AddSwaggerGen(
                  c =>
                  {
                      c.SwaggerDoc("v1", new OpenApiInfo
                      {
                          Title = "UserAccount API",
                          Version = "v1"
                      });
                      c.IncludeXmlComments(Path.Combine(
                          AppContext.BaseDirectory,
                          $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml"
                          ));
                  })
                //.AddDefaultHttpClient()
                .AddResponseCompression()
                .AddDataProtection();
                

            services.AddControllers();
            services
                .AddMvcCore()
                .AddDataAnnotations()
                .AddAuthorization()
                .AddApiExplorer()
               // .AddUnifiedRestApi()
                .AddCors();

            // Register Domain handler
             services     
                  .AddSingleton<IUserAccountProvider, UserAccountProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "userAccount v1");
                }
            );

            app.UseCors(options => options
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // //////////////////////////////////////////////////////////////////////////////////////////////////////

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.


        // ////////////////////////////////////////////////ytyryryrtyrtyrt
    }
}
