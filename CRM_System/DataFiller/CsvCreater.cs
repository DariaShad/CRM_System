using System.Text;
using CRM.DataLayer.Models;
using CsvHelper;
using Bogus;
using CRM.DataLayer;

namespace DataFiller
{
    public class CsvCreater
    {
        public static void CreateCsv()
        {
            StringBuilder csvContent= new StringBuilder();
            csvContent.AppendLine("FirstName; LastName; Patronymic; Birthday; Email; Phone; Passport; City; Address; Role; Password; RegistrationDate; IsDeleted");
            csvContent.AppendLine("Robert; Ivanov; Vladimirovich; 2000-04-13; robert@mail.ru; 89945673223; 1245969363; 2; Hollywood street 45; 2; 2334453; 2022-08-16; 0");
            string csvPath = "C:\\Users\\vit20\\Desktop\\xyz.csv";
            File.AppendAllText(csvPath, csvContent.ToString());
        }
        //IsDeleted Role Role
        public static List <LeadDto> FillList()
        {
            var testLeads = new Faker<LeadDto>()
            .RuleFor(l => l.FirstName, f => f.Person.FirstName)
            .RuleFor(l => l.LastName, f => f.Person.LastName)
            .RuleFor(l => l.Patronymic, f => f.Person.LastName)
            .RuleFor(l => l.Birthday, f => f.Person.DateOfBirth.Date)
            .RuleFor(l => l.Email, f => f.Person.Email)
            .RuleFor(l => l.Phone, f => f.Phone.PhoneNumber().Replace('.', '-'))
            .RuleFor(l => l.Passport, f => f.Phone.PhoneNumber().Remove('.'))
            .RuleFor(l => l.City, f => f.Random.Enum<City>())
            .RuleFor(l => l.Address, f => f.Person.Address.Street)
            .RuleFor(l => l.Password, f => f.Person.Random.Word())
            .RuleFor(l => l.RegistrationDate, f=> f.Person.DateOfBirth.Date);

            List<LeadDto> leads = new List<LeadDto>();
            leads.Capacity = 10;
            leads = testLeads.Generate(10);
            return leads;
        }
    }
}