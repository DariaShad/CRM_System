using CRM_System.BusinessLayer.Tests.TestModels;
using CRM_System.DataLayer;
using IncredibleBackend.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace CRM_System.BusinessLayer.Tests
{
    public class AccountsServicesTests
    {
        private AccountsService _sut;
        private Mock<ILeadsRepository> _leadsRepositoryMock;
        private Mock<IAccountsRepository> _accountsRepositoryMock;
        private ClaimModel _claimModel;
        private Mock<ILogger<AccountsService>> _logger;
        private Mock<IMessageProducer> _producer;

        [SetUp]
        public void Setup()
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _producer = new Mock<IMessageProducer>();
            _logger = new Mock<ILogger<AccountsService>>();
            _leadsRepositoryMock = new Mock<ILeadsRepository>();
            _sut = new AccountsService(_accountsRepositoryMock.Object, _logger.Object, _producer.Object, _leadsRepositoryMock.Object);
        }

        [Test]
        public async Task AddAccount_WhenDataIsCorrect_ReturnId()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            _leadsRepositoryMock.Setup(l => l.Add(It.Is<LeadDto>(p => p.Id == lead.Id)))
            .ReturnsAsync(lead.Id);
            var account = AccountDataForTest.GetAccountDataForTest();
            account.LeadId = lead.Id;
            _accountsRepositoryMock.Setup(a => a.AddAccount(It.Is<AccountDto>(p => p.Id == account.Id)))
            .ReturnsAsync(account.Id);

            _claimModel = new ClaimModel()
            {
                Id = lead.Id,
                Role = lead.Role
            };

            var expectedId = account.Id;

            //when
            var actual = await _sut.AddAccount(account, _claimModel);

            //then
            Assert.That(expectedId, Is.EqualTo(actual));
        }
    }
}
