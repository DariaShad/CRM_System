using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Controllers;
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
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigStorage>()));
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
        public void GetAllAccounts_ObjectResultPassed()
        {
            //given
            //when
            var actual=_sut.GetAllAccounts();

            //then
            var actualResult = actual.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, actualResult.StatusCode);
        }

    }
}
