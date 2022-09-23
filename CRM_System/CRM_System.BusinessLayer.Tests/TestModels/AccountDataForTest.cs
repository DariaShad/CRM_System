using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;

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
