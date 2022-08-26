using System.Text;
using CRM.DataLayer.Models;
using CsvHelper;
using Bogus;
using CRM.DataLayer;
using System.Data.SqlClient;
using System.Data;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Infrastucture;

namespace DataFiller
{
    public class BulkInsertService
    {
        public static List<AccountDto> FillListOfAccounts()
        {
            var testAccounts = new Faker<AccountDto>();
            //.RuleFor(a => a.Currency, c => c.Random.Enum<Currency>())
            //.RuleFor(a => a.Status, c => c.Random.Enum<AccountStatus>());

            List<AccountDto> accounts = new List<AccountDto>();

            accounts.Capacity = 400000;
            accounts = testAccounts.Generate(400000);
            int leadId = 4550001;
            foreach (AccountDto account in accounts)
            {
                account.Currency = Currency.JPY;
                account.Status = AccountStatus.Active;
                account.LeadId = leadId;
                account.IsDeleted = false;
                leadId++;
            }
            return accounts;
        }
        public static void BulkInsertAccounts()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Currency", typeof(Enum)));
            tbl.Columns.Add(new DataColumn("Status", typeof(Enum)));
            tbl.Columns.Add(new DataColumn("LeadId", typeof(int)));
            tbl.Columns.Add(new DataColumn("IsDeleted", typeof(bool)));

            List <AccountDto> accounts = FillListOfAccounts();
            for (int i = 0; i < accounts.Count; i++)
            {
                DataRow dr = tbl.NewRow();
                dr["Currency"] = accounts[i].Currency;
                dr["Status"] = accounts[i].Status;
                dr["LeadId"] = accounts[i].LeadId;
                dr["IsDeleted"] = accounts[i].IsDeleted;
                tbl.Rows.Add(dr);
            }

            string connection = @"Server=80.78.240.16;Database=CRM.Db;User Id=Student;Password=qwe!23";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "Account";
            //objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Currency", "Currency");
            objbulk.ColumnMappings.Add("Status", "Status");
            objbulk.ColumnMappings.Add("LeadId", "LeadId");
            objbulk.ColumnMappings.Add("IsDeleted", "IsDeleted");

            con.Open();
            objbulk.WriteToServer(tbl);
            con.Close();
        }

        public static List <LeadDto> FillListOfLeads()
        {
            var testLeads = new Faker<LeadDto>()
            .RuleFor(l => l.FirstName, f => f.Person.FirstName)
            .RuleFor(l => l.LastName, f => f.Person.LastName)
            .RuleFor(l => l.Patronymic, f => f.Person.FirstName)
            .RuleFor(l => l.Birthday, f => f.Person.DateOfBirth.Date)
            .RuleFor(l => l.Phone, f => f.Phone.PhoneNumberFormat())
   
            .RuleFor(l => l.Passport, f => f.Phone.PhoneNumberFormat().Replace("-", ""))
            .RuleFor(l => l.City, f => f.Random.Enum<City>())
            .RuleFor(l => l.Address, f => f.Person.Address.Street)
            .RuleFor(l => l.Password, f => f.Person.Random.Word())
            .RuleFor(l => l.RegistrationDate, f=> f.Person.DateOfBirth.Date);

            List<LeadDto> leads = new List<LeadDto>();
            leads.Capacity = 100000;
            leads = testLeads.Generate(100000);
            foreach (LeadDto lead in leads)
            {
                lead.Role = Role.Vip;
                lead.IsDeleted = false;
                lead.Passport = $"{lead.Passport[0]}{lead.Passport[1]}{lead.Passport[2]}{lead.Passport[3]} {lead.Passport[4]}{lead.Passport[5]}{lead.Passport[6]}{lead.Passport[7]}{lead.Passport[8]}{lead.Passport[9]}";
                lead.Password = PasswordHash.HashPassword(lead.Password);
                lead.Phone = $"8-{lead.Phone}";
                lead.Email = $"{Guid.NewGuid()}@mail.ru";
            }
            return leads;
        }
        public static void BulkInsertLeads()
        {
            DataTable tbl = new DataTable();

            //tbl.Columns.Add(new DataColumn("Id", typeof(int)));
            tbl.Columns.Add(new DataColumn("FirstName", typeof(string)));
            tbl.Columns.Add(new DataColumn("LastName", typeof(string)));
            tbl.Columns.Add(new DataColumn("Patronymic", typeof(string)));
            tbl.Columns.Add(new DataColumn("Birthday", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("Email", typeof(string)));
            tbl.Columns.Add(new DataColumn("Phone", typeof(string)));
            tbl.Columns.Add(new DataColumn("Passport", typeof(string)));
            tbl.Columns.Add(new DataColumn("City", typeof(Enum)));
            tbl.Columns.Add(new DataColumn("Address", typeof(string)));
            tbl.Columns.Add(new DataColumn("Role", typeof(Enum)));
            tbl.Columns.Add(new DataColumn("Password", typeof(string)));
            tbl.Columns.Add(new DataColumn("RegistrationDate", typeof(string)));
            tbl.Columns.Add(new DataColumn("IsDeleted", typeof(bool)));

            List <LeadDto> leads = FillListOfLeads();

            for (int i = 0; i < leads.Count; i++)
            {
                DataRow dr = tbl.NewRow();
                //dr["Id"] = i;
                dr["FirstName"] = leads[i].FirstName;
                dr["LastName"] = leads[i].LastName;
                dr["Patronymic"] = leads[i].Patronymic;
                dr["Birthday"] = leads[i].Birthday;
                dr["Email"] = leads[i].Email;
                dr["Phone"] = leads[i].Phone;
                dr["Passport"] = leads[i].Passport;
                dr["City"] = leads[i].City;
                dr["Address"] = leads[i].Address;
                dr["Role"] = leads[i].Role;
                dr["Password"] = leads[i].Password;
                dr["RegistrationDate"] = leads[i].RegistrationDate;
                dr["IsDeleted"] = leads[i].IsDeleted;

                tbl.Rows.Add(dr);
            }

            string connection = @"Server=80.78.240.16;Database=CRM.Db;User Id=Student;Password=qwe!23"; 
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "Lead";
            //objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("FirstName", "FirstName");
            objbulk.ColumnMappings.Add("LastName", "LastName");
            objbulk.ColumnMappings.Add("Patronymic", "Patronymic");
            objbulk.ColumnMappings.Add("Birthday", "Birthday");
            objbulk.ColumnMappings.Add("Email", "Email");
            objbulk.ColumnMappings.Add("Phone", "Phone");
            objbulk.ColumnMappings.Add("Passport", "Passport");
            objbulk.ColumnMappings.Add("City", "City");
            objbulk.ColumnMappings.Add("Address", "Address");
            objbulk.ColumnMappings.Add("Password", "Password");
            objbulk.ColumnMappings.Add("RegistrationDate", "RegistrationDate");
            objbulk.ColumnMappings.Add("IsDeleted", "IsDeleted");
            objbulk.ColumnMappings.Add("Role", "Role");
            con.Open();
            objbulk.WriteToServer(tbl);
            con.Close();

        }
        
    }
}