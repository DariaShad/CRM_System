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
        private ClaimModel _claimModel;
        private ILogger<LeadsService> _logger;
        private IRabbitMQProducer _producer;

        [SetUp]
        public void Setup()
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _claimModel = new ClaimModel();
            _leadsRepositoryMock = new Mock<ILeadsRepository>();
            _sut = new LeadsService(_leadsRepositoryMock.Object, _logger, _producer, _accountsRepositoryMock.Object);
        }

    [Test]
        public void AddLead_WhenDataIsCorrect_ReturnId()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            //when
            //then
        }
    }
}
