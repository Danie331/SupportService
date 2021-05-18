
using DomainEntities;
using System.Threading.Tasks;

namespace EmailProvider.Contract
{
    public interface IEmailServiceProvider
    {
        Task EnqueueAsync(NotificationMessage emailMessage);
    }
}
