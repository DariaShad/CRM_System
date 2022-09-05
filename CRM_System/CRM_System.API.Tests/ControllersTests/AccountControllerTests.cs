using AutoMapper;
using Castle.Core.Logging;
using CRM_System.API.Models.Requests;
using CRM_System.BusinessLayer;
using CRM_System.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CRM_System.API.Tests;

public class AccountControllerTests
{
    private AccountsController _sut;

    private Mock<IAccountsService> _accountsServiceMock;

    private IMapper _mapper;

    private ClaimModel _claims;

    private ILogger<AccountsController> _logger;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()));
        _claims = new ClaimModel();
        _accountsServiceMock= new Mock<IAccountsService>();
        _sut = new AccountsController(_accountsServiceMock.Object, _mapper, _logger);
    }

    [Test]
    public async Task GetAccount_ObjectResultPassed()
    {
        //given
        var accountId = 1;
        ClaimModel claimModel = new ClaimModel()
        {
            Id = accountId,
            Role = Role.Regular
        };
        _accountsServiceMock.Setup(a => a.GetAccountById(It.Is<int>(i => i == accountId), It.Is<ClaimModel>(c => c.Id==accountId))).ReturnsAsync(new AccountDto());
        
        //when
        var actual= await _sut.GetAccount(accountId);

        //then
        _accountsServiceMock.Verify(a => a.GetAccountById(It.Is<int>(i => i == accountId), It.Is<ClaimModel>(c => c.Id == accountId)), Times.Once);
        var actualResult = actual.Result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
    }

    [Test]
    public async Task GetAllAccountsByLeadId()
    {
        //given
        int leadId = 1;
        //when
        var actual = await _sut.GetAllAccountsByLeadId(leadId);

        //then
        var actualResult = actual.Result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
    }

    [Test]
    public async Task AddAccount_ValidRequestPassed_CreatedResultReceived()
    {
        //given
        var account = new AddAccountRequest
        {
            Currency = Currency.USD,
            Status = AccountStatus.Active,
            LeadId = 1
        };

        //when
        var actual = await _sut.AddAccount(account);

        //then
        var actualResult = actual.Result as CreatedResult;
        Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
    }

    [Test]
    public async Task UpdateAccount_NoContentResult()
    {
        //given
        var account = new UpdateAccountRequest
        {
            Status = AccountStatus.Active,
        };
        int accountId = 1;

        //when
        var actual = _sut.UpdateAccount(account, accountId);

        //then
        var actualResult = await actual as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
    }

    [Test]
    public async Task DeleteAccount_NoContentResult()
    {
        //given
        int accountId = 1;

        //when
        var actual = _sut.DeleteAccount(accountId);

        //then
        var actualResult = await actual as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
    }

}
