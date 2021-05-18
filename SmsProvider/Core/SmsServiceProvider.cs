using DomainEntities;
using SmsProvider.Contract;
using System.Threading.Tasks;

namespace SmsProvider.Core
{
    public class SmsServiceProvider : ISmsServiceProvider
    {
        public Task EnqueueAsync(NotificationMessage smsNotification)
        {
            // Do work
            return Task.CompletedTask;
        }
    }
}
