using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ePizzaHub.WebUI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Services
{
    public class AzureKeyVaultService : IAzureKeyVaultService
    {
        #region Members

        private readonly SecretClient _client;
        private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _env;

        #endregion Members

        #region Ctor

        public AzureKeyVaultService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

            var uri = _configuration["AzureKeyVault:Endpoint"];

            string keyVaultClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            // If necessary, create it.
            if (keyVaultClientId is null)
            {
                Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", _configuration["AzureKeyVault:ClientId"]);
            }
            string keyVaultClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
            // If necessary, create it.
            if (keyVaultClientSecret is null)
            {
                Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", _configuration["AzureKeyVault:ClientSecret"]);
            }
            string tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            // If necessary, create it.
            if (tenantId is null)
            {
                Environment.SetEnvironmentVariable("AZURE_TENANT_ID", _configuration["AzureKeyVault:TenantId"]);
            }
            _client = new SecretClient(vaultUri: new Uri(uri), credential: new DefaultAzureCredential());
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Retrieve a secret using the secret client.
        /// </summary>
        /// <param name="secretName"></param>
        /// <returns></returns>
        public async Task<string> GetSecret(string secretName)
        {
            var secret = await _client.GetSecretAsync(secretName);

            return secret.Value.Value;
        }

        /// <summary>
        /// Create a new secret using the secret client.
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="secretValue"></param>
        /// <returns></returns>
        public async Task<string> SetSecret(string secretName, string secretValue)
        {
            var secret = await _client.SetSecretAsync(secretName, secretValue);

            return secret.Value.Value;
        }

        /// <summary>
        /// Create a new secret using the secret client.
        /// </summary>
        /// <param name="secretName"></param>
        /// <returns></returns>
        public async Task<string> DeleteSecret(string secretName)
        {
            var operation = await _client.StartDeleteSecretAsync(secretName);
            var secret = await operation.WaitForCompletionAsync();

            _client.PurgeDeletedSecret(operation.Value.Name);

            return secret.Value.Value;
        }

        #endregion Methods
    }
}