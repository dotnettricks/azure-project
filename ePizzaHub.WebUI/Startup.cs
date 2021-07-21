using ePizzaHub.Services.Configuration;
using ePizzaHub.WebUI.Configuration;
using System.Configuration.Assemblies;
using ePizzaHub.Services.Models;
using ePizzaHub.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMarkupMin.AspNetCore5;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Core;
using Microsoft.Azure.Cosmos.Fluent;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Repositories.Implementations;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using ePizzaHub.Repositories;
using ePizzaHub.Services;
using System.Configuration.Assemblies;


namespace ePizzaHub.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            Env = env;
        }

        IWebHostEnvironment Env { get; }
        public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            /*
            IServiceCollection serviceCollections1 = services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(ConfigureDependencies.AddServices("DbConnectionCosmos")).GetAwaiter()
                .GetResult());
            */

            services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(configuration.GetSection("DbConnectionCosmos")).GetAwaiter().GetResult());
        
        ConfigureRepositories.AddServices(services, configuration);
            ConfigureDependencies.AddServices(services);
            services.AddSession();

            

            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<IPaymentQueueService, PaymentQueueService>();

            services.AddWebMarkupMin(options =>
            {
                options.AllowMinificationInDevelopmentEnvironment = true;
                options.AllowCompressionInDevelopmentEnvironment = true;
                options.DisablePoweredByHttpHeaders = true;
            }).AddHtmlMinification(options =>
            {
                options.MinificationSettings.RemoveRedundantAttributes = true;
                options.MinificationSettings.MinifyInlineJsCode = true;
                options.MinificationSettings.MinifyInlineCssCode = true;
                options.MinificationSettings.MinifyEmbeddedJsonData = true;
                options.MinificationSettings.MinifyEmbeddedCssCode = true;
            })
              .AddHttpCompression();

            var builder = services.AddControllersWithViews();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "RedisItems_";
            });
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            services.Configure<RazorPayConfig>(configuration.GetSection("RazorPayConfig"));

            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer"));
            });

            services.AddDbContext<AppDbContextCosmos>(options =>
            {
                options.UseCosmos("DbConnectionCosmos", "SaveCart");
             });

        }
    



           // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 7;
                    ctx.Context.Response.Headers["cache-control"] =
                        "public, max-age=" + durationInSeconds;
                }
            });
            
            app.UseWebMarkupMin();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication(); //for accessing user info

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Cart}/{action=AddToCart}/{id?}");

            });

                
        }
  
    
        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosDbService;
        }
    
        /*
      private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
      {
          var databaseName = configurationSection["DatabaseName"];
          var containerName = configurationSection["ContainerName"];
          var account = configurationSection["Account"];
          var key = configurationSection["Key"];
          var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
          var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
          await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
          var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
          return cosmosDbService;
      }
      */
}
}

