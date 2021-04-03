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
using UserAccount.Infrastructure.Repository;

namespace UserAccount.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        private const int CacheMemorySize = 350000000;

        //   public IConfiguration Configuration { get; }
        //   public IWebHostEnvironment Env { get; }

        public Startup(IHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            // _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            //connectionStrings
            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }
        // variable d'environnement stockée sur azure
        private string GetConnectionString(string connectionString)
        {
            if (_environment.IsDevelopment())
            {
                return StartupConstantDev.ConnectionString;
            }
            else
            {
                return new Uri(Environment.GetEnvironmentVariable(connectionString)).ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "UserAccountAPI",
                        Version = "v1",
                        Description = "The UserAccount API  allows you to manage a list of users."

                    });
                    c.IncludeXmlComments(Path.Combine(
                        AppContext.BaseDirectory,
                        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
                            ));
                });

            services
                .AddHealthChecks()
                .AddCheck("Default", () => HealthCheckResult.Healthy("OK"))
            ;
            var options = Options.Create(new DbClientOptions
            {
                LittleBigPlanetData = () => new SqlConnection(GetConnectionString("DATABASE_URL"))
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
            services.AddControllers();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //.AddDefaultHttpClient()
            services.AddResponseCompression();
            // .AddDataProtection();          
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAccountAPI");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                // app.UseWebApiExceptionHandler();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseCors(options => options
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks();
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
