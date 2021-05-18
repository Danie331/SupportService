
using DomainEntities;
using EmailProvider.Contract;
using SmsProvider.Contract;
using SupportRequestService;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SupportRequestServiceCore
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly IEmailServiceProvider _emailService;
        private readonly ISmsServiceProvider _smsService;
        public SupportRequestService(IEmailServiceProvider emailService,
                                     ISmsServiceProvider smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task AddAsync(SupportRequest supportRequest)
        {
            var emailNotification = BuildEmailMessage(supportRequest);
            await _emailService.EnqueueAsync(emailNotification);

            if (supportRequest.SupportCaseStatus == SupportCaseStatusEnum.SameBusinessDay)
            {
                var smsRecipients = new[] { GetCampaignManager(), GetProductSupportPerson(supportRequest.ProductId) };
                if (GetSupportPeriod() == SupportTimePeriodEnum.AfterHours)
                {
                    var smsNotification = new NotificationMessage { MessageContent = supportRequest.RequestContent, Recipients = smsRecipients };
                    await _smsService.EnqueueAsync(smsNotification);
                }
            }
        }

        public async Task UpdateAsync(SupportRequest supportRequest)
        {
            var emailNotification = BuildEmailMessage(supportRequest);
            await _emailService.EnqueueAsync(emailNotification);

            if (supportRequest.SupportCaseStatus == SupportCaseStatusEnum.SameBusinessDay)
            {
                var smsRecipients = new[] { GetProductSupportPerson(supportRequest.ProductId) };
                if (GetSupportPeriod() == SupportTimePeriodEnum.AfterHours)
                {
                    var smsNotification = new NotificationMessage { MessageContent = supportRequest.RequestContent, Recipients = smsRecipients };
                    await _smsService.EnqueueAsync(smsNotification);
                }
            }
        }

        public async Task CloseAsync(SupportRequest supportRequest)
        {
            var emailNotification = BuildEmailMessage(supportRequest);
            await _emailService.EnqueueAsync(emailNotification);
        }

        private NotificationMessage BuildEmailMessage(SupportRequest supportRequest)
        {
            var message = supportRequest.RequestContent;
            if (GetSupportPeriod() == SupportTimePeriodEnum.OutOfBusinessHours)
            {
                message += " - This case is being logged outside of business hours and will be handled as soon as possible.";
            }

            return new NotificationMessage
            {
                MessageContent = message,
                Recipients = GetEmailRecipients(supportRequest)
            };
        }

        private IEnumerable<string> GetEmailRecipients(SupportRequest supportRequest)
        {
            return new[]
            {
                GetCampaignManager(),
                GetProductSupportPerson(supportRequest.ProductId),
                supportRequest.ClientEmailAddress
            }.Concat(supportRequest.ClientCCList);
        }

        private SupportTimePeriodEnum GetSupportPeriod()
        {
            var weekDays = DayOfWeek.Monday & DayOfWeek.Tuesday & DayOfWeek.Wednesday & DayOfWeek.Thursday & DayOfWeek.Friday;
            if (DateTime.Today.DayOfWeek.HasFlag(weekDays))
            {
                if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour <= 17)
                {
                    return SupportTimePeriodEnum.BusinessHours;
                }

                if (DateTime.Now.Hour > 17 || DateTime.Now.Hour <= 3)
                {
                    return SupportTimePeriodEnum.AfterHours;
                }
            }

            return SupportTimePeriodEnum.OutOfBusinessHours;
        }

        private string GetProductSupportPerson(int productId) => "supportperson@product.com";

        private string GetCampaignManager() => "campaignmanager@test.com";
    }
}
