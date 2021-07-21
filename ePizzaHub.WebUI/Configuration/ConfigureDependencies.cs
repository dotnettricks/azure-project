using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace ePizzaHub.WebUI.Configuration
{
    public class ConfigureDependencies
    {
       /*
        public System.Configuration IConfiguration configuration;
        
        public ConfigureDependencies(IConfiguration configuration1)
        {
            configuration = configuration1;
        }
       */
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserAccessor, UserAccessor>();
            services.AddTransient<IFileHelper, FileHelper>();
            /*
            services.AddSingleton<ICosmosDbService, CosmosDbService>(configuration.GetConnectionString("DbConnectionCosmos")
        );
            */
          
        }

        /*
        internal static IConfigurationSection AddServices(string v)
        {
            throw new NotImplementedException();
        }
        */
        
    }
}
