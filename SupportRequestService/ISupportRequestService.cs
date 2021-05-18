
using DomainEntities;
using System.Threading.Tasks;

namespace SupportRequestService
{
    public interface ISupportRequestService
    {
        Task AddAsync(SupportRequest supportRequest);
        Task UpdateAsync(SupportRequest supportRequest);
        Task CloseAsync(SupportRequest supportRequest);
    }
}
