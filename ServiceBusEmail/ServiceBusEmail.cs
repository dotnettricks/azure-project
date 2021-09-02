using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;

namespace ServiceBusEmail
{
    public static class ServiceBusEmail
    {
        [FunctionName("SendEmailServiceBusTrigger")]
        public static void Run([ServiceBusTrigger("emailqueue", Connection = "ServiceBusConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            var deserializedMessage = JsonConvert.DeserializeObject<User>(myQueueItem);
            string smtpName = Environment.GetEnvironmentVariable("smtp");
            string email = Environment.GetEnvironmentVariable("email");
            string emPassword = Environment.GetEnvironmentVariable("password");
            string receipeient = deserializedMessage.Email;
            string user = deserializedMessage.UserName;

            try
            {
                var smtpClient = new SmtpClient(smtpName)
                {
                    Port = Convert.ToInt32(Environment.GetEnvironmentVariable("port")),
                    Credentials = new NetworkCredential(email, emPassword),
                    EnableSsl = true
                };
                smtpClient.Send(email, receipeient, "Welcome Aboard!!", $"Dear {user} a warm welcome from us at ePizzaHub!!");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            log.LogInformation($"Successfully sent!!");
        }
    }

    public class User
    {
        public string UserName;
        public string Email;
    }
}