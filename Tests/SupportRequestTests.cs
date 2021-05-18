
using NUnit.Framework;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SupportRequestService;
using EmailProvider.Contract;
using EmailProvider.Core;
using SmsProvider.Contract;
using SmsProvider.Core;

namespace Tests
{
    [TestFixture]
    public class SupportRequestTests
    {

        private IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceProviderCollection = new ServiceCollection()
                                                .AddScoped<IEmailServiceProvider, EmailServiceProvider>()
                                                .AddScoped<ISmsServiceProvider, SmsServiceProvider>()
                                                .AddScoped<ISupportRequestService, SupportRequestServiceCore.SupportRequestService>();
            _serviceProvider = serviceProviderCollection.BuildServiceProvider();
        }

        [Test]
        public async Task Test_Add()
        {
            var service = _serviceProvider.GetService<ISupportRequestService>();

            await service.AddAsync(new DomainEntities.SupportRequest
            {
                ClientCCList = new string[] { },
                ClientEmailAddress = "test@test.com",
                ProductId = 1,
                RequestContent = "This is a Test",
                SupportCaseStatus = DomainEntities.SupportCaseStatusEnum.SameBusinessDay
            });

            Assert.Pass();
        }

        [Test]
        public async Task Test_Update()
        {
            var service = _serviceProvider.GetService<ISupportRequestService>();

            await service.UpdateAsync(new DomainEntities.SupportRequest
            {
                ClientCCList = new string[] { },
                ClientEmailAddress = "test@test.com",
                ProductId = 1,
                RequestContent = "This is a Test",
                SupportCaseStatus = DomainEntities.SupportCaseStatusEnum.SameBusinessDay
            });

            Assert.Pass();
        }

        [Test]
        public async Task Test_Close()
        {
            var service = _serviceProvider.GetService<ISupportRequestService>();

            await service.CloseAsync(new DomainEntities.SupportRequest
            {
                ClientCCList = new string[] { },
                ClientEmailAddress = "test@test.com",
                ProductId = 1,
                RequestContent = "This is a Test",
                SupportCaseStatus = DomainEntities.SupportCaseStatusEnum.SameBusinessDay
            });

            Assert.Pass();
        }
    }
}
