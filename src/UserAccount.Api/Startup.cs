using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccount.Infrastructure;

namespace UserAccount.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        // private const int CacheMemorySize = 350000000;

        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck("Default", () => HealthCheckResult.Healthy("OK"))
            ;

            var options = Options.Create(new DbClientOptions
            {
                LittleBigPlanetData = () => new SqlConnection(_configuration.GetConnectionString("LittlebigPlanetData")),
            });
            services.AddTransient(x => options);

            //ProviderConfigurationDictionary configurations = new ProviderConfigurationDictionary();
            //configurations.ProviderConfigurations = GetProvidersConfig();
            //services.AddTransient(x => configurations);

            services
                // .AddCustomSwaggerGen<Startup>()
                // .AddDefaultHttpClient()
                .AddResponseCompression()
                .AddDataProtection();

            services
                .AddMvcCore()
                .AddDataAnnotations()
                .AddAuthorization()
                .AddApiExplorer()
                //.AddUnifiedRestApi()
                .AddCors();

            // Register Domain handler
            /* services     
                  .AddSingleton<IUserProvider, UserProvider>();
                  .AddSingleton<ISupervisorRepository, SupervisorRepository>(); */
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
