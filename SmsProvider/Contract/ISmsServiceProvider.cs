
using DomainEntities;
using System.Threading.Tasks;

namespace SmsProvider.Contract
{
    public interface ISmsServiceProvider
    {
        Task EnqueueAsync(NotificationMessage smsNotification);
    }
}
