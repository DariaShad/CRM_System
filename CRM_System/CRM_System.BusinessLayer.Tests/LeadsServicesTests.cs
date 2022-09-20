using CRM_System.API.Producer;
using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Tests
{
    public class LeadsServicesTests
    {
        //given
        //when
        //then
        private LeadsService _sut;
        private Mock<ILeadsRepository> _leadsRepositoryMock;
        private Mock<IAccountsRepository> _accountsRepositoryMock;
        private Mock <ClaimModel> _claimModel;
        private Mock <ILogger<LeadsService>> _logger;
        private Mock <IRabbitMQProducer> _producer;

        [SetUp]
        public void Setup()
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _claimModel = new Mock <ClaimModel>();
            _logger = new Mock <ILogger<LeadsService>>();
            _leadsRepositoryMock = new Mock<ILeadsRepository>();
            _sut = new LeadsService(_leadsRepositoryMock.Object, _logger.Object, _producer.Object, _accountsRepositoryMock.Object);
        }

    [Test]
        public async Task AddLead_WhenDataIsCorrect_ReturnId()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            _leadsRepositoryMock.Setup(l => l.Add(It.Is<LeadDto>(p => p.Id== lead.Id)))
            .ReturnsAsync(lead.Id);
            var expectedId= lead.Id;

            //when
            var actual = await _sut.Add(lead);

            //then
            Assert.AreEqual(expectedId, actual);
        }
    }
}
