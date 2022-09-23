using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;

namespace CRM_System.BusinessLayer.Tests
{
    public static class LeadDataForTest
    {
        public static LeadDto GetRegularLeadDataForTest()
        {
            var lead = new LeadDto()
            {
                FirstName = "Lala",
                LastName = "Lala",
                Patronymic = "Lala",
                Email = "lalalalf@mail.ru",
                Address = "Lenina street, 34-3",
                City = City.Moscow,
                Passport = "2020 400400",
                Password = "1234569",
                Phone = "89996321578",
                Birthday = DateTime.Parse("2000 - 12 - 12"),
                RegistrationDate = DateTime.Now,
                Role = Role.Regular,
                IsDeleted = false,
                Accounts = new List<AccountDto>()
            };
            return lead;

        }

        public static LeadDto GetVipLeadDataForTest()
        {
            var lead = new LeadDto()
            {
                FirstName = "Lala",
                LastName = "Lala",
                Patronymic = "Lala",
                Email = "lalalalf@mail.ru",
                Address = "Lenina street, 34-3",
                City = City.Moscow,
                Passport = "2020 400400",
                Password = "1234569",
                Phone = "89996321578",
                Birthday = DateTime.Parse("2000 - 12 - 12"),
                RegistrationDate = DateTime.Now,
                Role = Role.Vip,
                IsDeleted = false,
                Accounts = new List<AccountDto>()
            };
            return lead;


        }
    }
}
