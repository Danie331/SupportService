
using System.Collections.Generic;

namespace DomainEntities
{
    public class NotificationMessage
    {
        public IEnumerable<string> Recipients { get; set; }
        public string MessageContent { get; set; }
    }
}
