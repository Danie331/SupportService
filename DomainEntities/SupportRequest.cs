
using System.Collections.Generic;

namespace DomainEntities
{
    public class SupportRequest
    {
        public int? SupportRequestId { get; set; }
        public int ProductId { get; set; }
        public SupportCaseStatusEnum SupportCaseStatus { get; set; }
        public string RequestContent { get; set; }
        public string ClientEmailAddress { get; set; }
        public IEnumerable<string> ClientCCList { get; set; }
    }
}
