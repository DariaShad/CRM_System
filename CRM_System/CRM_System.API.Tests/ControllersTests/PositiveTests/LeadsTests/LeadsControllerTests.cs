using AutoMapper;
using CRM_System.BusinessLayer;
using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CRM_System.API.Tests;

public class LeadsControllerTests
{
    private Mock<ILeadsService> _leadsServiceMock;
    private LeadsController _sut;
    private IMapper _mapper;
    private readonly ILogger<LeadsController> _logger;
    private ClaimModel _claims;

    [SetUp]
    public void SetUp()
    {
        _leadsServiceMock = new Mock<ILeadsService>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()));
        _sut = new LeadsController(_leadsServiceMock.Object, _mapper, _logger);
        _claims = new();
    }

    [Test]
    public async Task AddTest_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        var expectedId = 1;

        var lead = new LeadRegistrationRequest
        {
            FirstName = "Nathalie",
            LastName = "Bahadur",
            Patronymic = "Efrat",
            Birthday = DateTime.Parse("1970-04-24"),
            Email = "Nied1961@gmail.com",
            Phone = "+71759064306",
            Passport = "17120063561",
            City = City.Kazan,
            Address = "Paseo Atencio, 050",
            Password = "a$i9QSVc6B"
        };

        _leadsServiceMock.Setup(l => l.Add(It.IsAny<LeadDto>()))
            .ReturnsAsync(1);

        //when
        var actual = await _sut.Register(lead);

        //then
        var actualResult = actual.Result as CreatedResult;
        var actualLead = actualResult.Value as LeadDto;

        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        Assert.True((int)actualResult.Value == expectedId);

        _leadsServiceMock.Verify(l => l.Add(It.Is<LeadDto>(l =>
            l.FirstName == lead.FirstName &&
            l.LastName == lead.LastName &&
            l.Patronymic == lead.Patronymic &&
            l.Birthday == lead.Birthday &&
            l.Email == lead.Email &&
            l.Phone == lead.Phone &&
            l.Passport == lead.Passport &&
            l.City == lead.City &&
            l.Address == lead.Address &&
            l.Password == lead.Password &&
            !l.IsDeleted
            )), Times.Once);
    }

    [Test]
    public async Task GetByIdTest_ValidRequestPassed_OkResultReceived()
    {
        //given
        var lead = new LeadDto()
        {
            Id = 4,
            FirstName = "Delphine",
            LastName = "Skyler",
            Patronymic = "Richard",
            Birthday = DateTime.Parse("1976-10-06"),
            Email = "Lecought92@gmail.com",
            Phone = "+78500146929",
            Passport = "0893 227130",
            City = City.Ufa,
            Address = "Clemmie Isle Suite 139",
            Role = Role.Regular,
            Password = "BWL|xfzZT}",
            RegistrationDate = DateTime.Parse("2015-02-12"),
            IsDeleted = false
        };

        _leadsServiceMock.Setup(l => l.GetById(lead.Id, It.IsAny<ClaimModel>()))
            .ReturnsAsync(lead);

        //when
        var actual = await _sut.GetById(lead.Id);

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualLead = actualResult.Value as LeadMainInfoResponse;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(lead.Id, actualLead.Id);
        Assert.AreEqual(lead.FirstName, actualLead.FirstName);
        Assert.AreEqual(lead.LastName, actualLead.LastName);
        Assert.AreEqual(lead.Patronymic, actualLead.Patronymic);
        Assert.AreEqual(lead.Birthday, actualLead.Birthday);
        Assert.AreEqual(lead.City, actualLead.City);

        _leadsServiceMock.Verify(l => l.GetById(lead.Id, It.IsAny<ClaimModel>()), Times.Once);
    }

    [Test]
    public async Task GetAllTest_ValidRequestPassed_OkResultReceived()
    {
        //given
        var leads = new List<LeadDto>()
            {
                new LeadDto()
                {
                    Id = 1,
                    FirstName = "Shyam",
                    LastName = "Olander",
                    Patronymic = "Beverley",
                    Birthday = DateTime.Parse("1971-10-21"),
                    Email = "Reate1940@gmail.com",
                    Phone = "+79599980386",
                    Passport = "688226706560",
                    City = City.Ufa,
                    Address = "Clemmie Isle Suite 139",
                    Role = Role.Regular,
                    Password = "BWL|xfzZT}",
                    RegistrationDate = DateTime.Parse("2015-02-12"),
                    IsDeleted = false
                },

                new LeadDto()
                {
                    Id = 2,
                    FirstName = "Nikostratos",
                    LastName = "Erikas",
                    Patronymic = "Bernadette",
                    Birthday = DateTime.Parse("1973-06-16"),
                    Email = "Staing85@gmail.com",
                    Phone = "+73234343178",
                    Passport = "688226706560",
                    City = City.Tolyatti,
                    Address = "1663 Gateway Avenue",
                    Role = Role.Vip,
                    Password = "*tLsR@ZE%s",
                    RegistrationDate = DateTime.Parse("2015-05-08"),
                    IsDeleted = false
                },

                new LeadDto()
                {
                    Id = 3,
                    FirstName = "Koch",
                    LastName = "Harisha",
                    Patronymic = "Vera",
                    Birthday = DateTime.Parse("1976-01-13"),
                    Email = "Kied1958@gmail.com",
                    Phone = "+79429769100",
                    Passport = "75752325228",
                    City = City.Samara,
                    Address = "1749 Coal Road",
                    Role = Role.Vip,
                    Password = "*tLsR@ZE%s",
                    RegistrationDate = DateTime.Parse("2015-07-05"),
                    IsDeleted = true
            }
        };

        _leadsServiceMock.Setup(l => l.GetAll())
            .ReturnsAsync(leads);

        //when
        var actual = await _sut.GetAll();

        //then
        var actualResult = actual.Result as OkObjectResult;
        var actualLeads = actualResult?.Value as List<LeadAllInfoResponse>;

        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        Assert.AreEqual(leads.Count, actualLeads.Count);
        Assert.AreEqual(leads[0].Id, actualLeads[0].Id);
        Assert.AreEqual(leads[0].FirstName, actualLeads[0].FirstName);
        Assert.AreEqual(leads[0].LastName, actualLeads[0].LastName);
        Assert.AreEqual(leads[0].Patronymic, actualLeads[0].Patronymic);
        Assert.AreEqual(leads[0].Birthday, actualLeads[0].Birthday);
        Assert.AreEqual(leads[0].City, actualLeads[0].City);

        _leadsServiceMock.Verify(l => l.GetAll(), Times.Once);
    }

    [Test]
    public async Task UpdateTest_ValidRequestPassed_NoContentResultReceived()
    {
        //given
        var lead = new LeadDto
        {
            Id = 5,
            FirstName = "Jesusa",
            LastName = "Ziyad",
            Patronymic = "Inge",
            Birthday = DateTime.Parse("1977-09-08"),
            Email = "Icund19@gmail.com",
            Phone = "+70233587763",
            Passport = "7736 059842",
            City = City.Krasnodar,
            Address = "3514 Farnum Road",
            Role = Role.Vip,
            Password = "4@yn|$Cd|O",
            RegistrationDate = DateTime.Parse("2017-03-25"),
            IsDeleted = false
        };

        var newLead = new LeadUpdateRequest
        {
            FirstName = "Danilo",
            LastName = "Oberst",
            Patronymic = "Rebekka",
            Birthday = DateTime.Parse("1977-12-27"),
            Phone = "+76547844839",
            City = City.Saratov,
            Address = "409 Willow Greene Drive"
        };

        _leadsServiceMock.Setup(l => l.Update(lead, lead.Id, _claims));

        //when
        var actual = await _sut.Update(newLead, lead.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);

        _leadsServiceMock.Verify(l => l.Update(It.Is<LeadDto>(l =>
        l.FirstName == newLead.FirstName &&
        l.LastName == newLead.LastName &&
        l.Patronymic == newLead.Patronymic &&
        l.Birthday == newLead.Birthday &&
        l.Phone == newLead.Phone &&
        l.City == newLead.City &&
        l.Address == newLead.Address
        ), lead.Id, It.IsAny<ClaimModel>()), Times.Once);
    }

    [Test]
    public async Task RemoveTest_ValidRequestPassed_NoContentResultReceived()
    {
        //given
        var lead = new LeadDto
        {
            Id = 6,
            FirstName = "Asmaa",
            LastName = "Pavlina",
            Patronymic = "Marlena",
            Birthday = DateTime.Parse("1979-11-07"),
            Email = "Insch9@gmail.com",
            Phone = "+77221007356",
            Passport = "7716 755288",
            City = City.Chelyabinsk,
            Address = "957 Simpson Street",
            Role = Role.Vip,
            Password = "na9}UeC5nY",
            RegistrationDate = DateTime.Parse("2017-10-10"),
            IsDeleted = false
        };

        _leadsServiceMock.Setup(l => l.GetById(lead.Id, It.IsAny<ClaimModel>()))
            .ReturnsAsync(lead);

        //when
        var actual = await _sut.Remove(lead.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        //Assert.IsTrue(lead.IsDeleted);

        _leadsServiceMock.Verify(l => l.Delete(lead.Id, true, It.IsAny<ClaimModel>()), Times.Once);
    }

    [Test]
    public async Task RestoreTest_ValidRequestPassed_NoContentResultReceived()
    {
        //given
        var lead = new LeadDto
        {
            Id = 6,
            FirstName = "Asmaa",
            LastName = "Pavlina",
            Patronymic = "Marlena",
            Birthday = DateTime.Parse("1979-11-07"),
            Email = "Insch9@gmail.com",
            Phone = "+77221007356",
            Passport = "7716 755288",
            City = City.Chelyabinsk,
            Address = "957 Simpson Street",
            Role = Role.Vip,
            Password = "na9}UeC5nY",
            RegistrationDate = DateTime.Parse("2017-10-10"),
            IsDeleted = true
        };

        _leadsServiceMock.Setup(l => l.GetById(lead.Id, It.IsAny<ClaimModel>()))
            .ReturnsAsync(lead);

        //when
        var actual = await _sut.Remove(lead.Id);

        //then
        var actualResult = actual as NoContentResult;

        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        //Assert.IsFalse(lead.IsDeleted);

        _leadsServiceMock.Verify(l => l.Restore(lead.Id, false, It.IsAny<ClaimModel>()), Times.Once);
    }
}
