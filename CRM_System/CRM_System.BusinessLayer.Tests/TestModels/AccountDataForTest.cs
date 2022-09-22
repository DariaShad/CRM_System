using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Tests.TestModels
{
    public static class AccountDataForTest
    {
        public static AccountDto GetAccountDataForTest()
        {
            var account = new AccountDto()
            {
                Currency = TradingCurrency.RUB,
                Status = AccountStatus.Active,
                IsDeleted = false
            };
            return account;
        }
    }
}
