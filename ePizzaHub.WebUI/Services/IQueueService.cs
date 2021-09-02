using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Services
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
    }
}