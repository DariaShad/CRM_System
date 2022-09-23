using CRM_System.DataLayer;
using FluentAssertions;
using IncredibleBackend.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

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
        private Mock <ILogger<LeadsService>> _logger;
        private Mock <IMessageProducer> _producer;

        [SetUp]
        public void Setup()
        {
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _producer = new Mock <IMessageProducer>();
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
            Assert.That(expectedId, Is.EqualTo( actual));
        }

        [Test]
        public async Task AddLead_WhenEmailIsNotUnique_ReturnNotUniqueEmailException()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            lead.Email = "strinываываg@vx.ru";
            _leadsRepositoryMock.Setup(l => l.GetByEmail("strinываываg@vx.ru")).ReturnsAsync(lead);
            var expectedId = lead.Id;

            //when

            //then
            Assert.ThrowsAsync<Exceptions.NotUniqueEmailException>(() => _sut.Add(lead));
            _leadsRepositoryMock.Verify( l=> l.Add(It.IsAny<LeadDto>()), Times.Never());
        }

        [Test]
        public async Task GetLeadById_WhenDataIsCorrect_ReturnLead()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            _claimModel = new ClaimModel()
            {
                Id = lead.Id,
                Role = lead.Role
            };
            _leadsRepositoryMock.Setup(l => l.GetById(lead.Id)).ReturnsAsync(lead);
            var expected = lead;
            //when

            var actual = await _sut.GetById(lead.Id, _claimModel);

            //then
            expected.Should().BeEquivalentTo(actual);
            _leadsRepositoryMock.Verify(l => l.GetById(lead.Id), Times.Once);
        }

        [Test]
        public async Task GetLeadById_WhenLeadWasNotFound_ReturnNotFoundException()
        {
            //given
            LeadDto lead = new LeadDto()
            {
                IsDeleted = true,
            };
            _leadsRepositoryMock.Setup(l => l.GetById(lead.Id)).ReturnsAsync(lead);
            //when
            _claimModel = new ClaimModel()
            {
                Id = lead.Id,
                Role = lead.Role
            };

            //then
            Assert.ThrowsAsync<Exceptions.NotFoundException>(() => _sut.GetById(lead.Id, _claimModel));
        }

        [Test]
        public async Task GetLeadByEmail_WhenDataIsCorrect_ReturnLead()
        {
            //given
            var lead = LeadDataForTest.GetRegularLeadDataForTest();

            _leadsRepositoryMock.Setup(l => l.GetByEmail(lead.Email)).ReturnsAsync(lead);
            var expected = lead;
            //when

            var actual = await _sut.GetByEmail(lead.Email);

            //then
            expected.Should().BeEquivalentTo(actual);
            _leadsRepositoryMock.Verify(l => l.GetByEmail(lead.Email), Times.Once);
        }

        [Test]
        public async Task GetLeadByEmail_WhenLeadWasNotFound_ReturnNotFoundException()
        {
            //given
            LeadDto lead = new LeadDto()
            {
                IsDeleted = true,
            };
            _leadsRepositoryMock.Setup(l => l.GetByEmail(lead.Email)).ReturnsAsync((LeadDto)null);
            //when

            //then
            Assert.ThrowsAsync<Exceptions.NotFoundException>(() => _sut.GetByEmail(lead.Email));
        }

        [Test]
        public async Task UpdateLead_WhenDataIsCorrect_UpdateLead()
        {
            //given
            int id = 2;
            var lead = LeadDataForTest.GetRegularLeadDataForTest();
            lead.Id = id;

            var expectedLead = LeadDataForTest.GetRegularLeadDataForTest();
            expectedLead.Id = id;

            _leadsRepositoryMock.Setup(l => l.GetById(lead.Id)).ReturnsAsync(lead);
            _leadsRepositoryMock.Setup(l => l.Update(lead));
            _claimModel = new()
            {
                Id = id,
                Role = lead.Role
            };
            //when

             await _sut.Update(lead, id, _claimModel);

            //then
            _leadsRepositoryMock.Verify(l => l.GetById(It.Is<int>(i => i == id)), Times.Once);

        }

    }
}
