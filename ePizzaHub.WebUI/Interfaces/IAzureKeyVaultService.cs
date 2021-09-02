using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Interfaces
{
    public interface IAzureKeyVaultService
    {
        Task<string> GetSecret(string secretName);

        Task<string> SetSecret(string secretName, string secretValue);

        Task<string> DeleteSecret(string secretName);
    }
}
