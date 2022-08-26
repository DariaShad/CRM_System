using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.API.Controllers;
using CRM_System.API.Models.Requests;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.API.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        private AccountsController _sut;

        private Mock<IAccountsService> _accountsServiceMock;

        private IMapper _mapper;

        private ClaimModel _claims;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()));
            _claims = new ClaimModel();
            _accountsServiceMock= new Mock<IAccountsService>();
            _sut = new AccountsController(_accountsServiceMock.Object, _mapper);
        }

        [Test]
        public void GetAccount_ObjectResultPassed()
        {
            //given
            var accountId = 1;
            _accountsServiceMock.Setup(a => a.GetAccountById(It.Is<int>(i => i == accountId))).Returns(new AccountDto());
            
            //when
            var actual=_sut.GetAccount(accountId);

            //then
            _accountsServiceMock.Verify(a => a.GetAccountById(It.Is<int>(i => i == accountId)), Times.Once);
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

        [Test]
        public void GetAllAccountsByLeadId()
        {
            //given
            int leadId = 1;
            //when
            var actual = _sut.GetAllAccountsByLeadId(leadId);

            //then
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

        [Test]
        public void AddAccount_ValidRequestPassed_CreatedResultReceived()
        {
            //given
            var account = new AddAccountRequest
            {
                Currency = Currency.USD,
                Status = AccountStatus.Active,
                LeadId = 1
            };

            //when
            var actual = _sut.AddAccount(account);

            //then
            var actualResult = actual.Result as CreatedResult;
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
        }

        [Test]
        public void UpdateAccount_NoContentResult()
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
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }

        [Test]
        public void DeleteAccount_NoContentResult()
        {
            //given
            int accountId = 1;

            //when
            var actual = _sut.DeleteAccount(accountId);

            //then
            var actualResult = actual as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, actualResult.StatusCode);
        }

    }
}
