
using DomainEntities;
using EmailProvider.Contract;
using System.Threading.Tasks;

namespace EmailProvider.Core
{
    public class EmailServiceProvider : IEmailServiceProvider
    {
        public Task EnqueueAsync(NotificationMessage emailMessage)
        {
            // Do work
            return Task.CompletedTask;
        }
    }
}
