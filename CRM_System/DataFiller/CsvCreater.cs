using System.Text;
using CRM.DataLayer.Models;
using CsvHelper;
using Bogus;
using CRM.DataLayer;
using System.Data.SqlClient;
using System.Data;

namespace DataFiller
{
    public class CsvCreater
    {
        //public static void CreateCsv()
        //{
        //    StringBuilder csvContent= new StringBuilder();
        //    csvContent.AppendLine("FirstName; LastName; Patronymic; Birthday; Email; Phone; Passport; City; Address; Role; Password; RegistrationDate; IsDeleted");
        //    csvContent.AppendLine("Robert; Ivanov; Vladimirovich; 2000-04-13; robert@mail.ru; 89945673223; 1245969363; 2; Hollywood street 45; 2; 2334453; 2022-08-16; 0");
        //    string csvPath = "C:\\Users\\vit20\\Desktop\\xyz.csv";
        //    File.AppendAllText(csvPath, csvContent.ToString());
        //}
        //IsDeleted Role Role
        public static List <LeadDto> FillList()
        {
            var testLeads = new Faker<LeadDto>()
            .RuleFor(l => l.FirstName, f => f.Person.FirstName)
            .RuleFor(l => l.LastName, f => f.Person.LastName)
            .RuleFor(l => l.Patronymic, f => f.Person.LastName)
            .RuleFor(l => l.Birthday, f => f.Person.DateOfBirth.Date)
            .RuleFor(l => l.Email, f => f.Person.Email)
            //.RuleFor(l => l.Phone, f => f.Phone.PhoneNumber())
            //.RuleFor(l => l.Passport, f => f.Phone.PhoneNumber())
            .RuleFor(l => l.City, f => f.Random.Enum<City>())
            .RuleFor(l => l.Address, f => f.Person.Address.Street)
            .RuleFor(l => l.Password, f => f.Person.Random.Word())
            .RuleFor(l => l.RegistrationDate, f=> f.Person.DateOfBirth.Date);

            List<LeadDto> leads = new List<LeadDto>();
            leads.Capacity = 100000;
            leads = testLeads.Generate(100000);
            foreach (LeadDto lead in leads)
            {
                lead.Role = Role.Regular;
                lead.IsDeleted = false;
                lead.Passport = "4424837625";
                lead.Phone = "89992483762";
            }
            return leads;
        }

        public static void BulkInsert()
        {
            DataTable tbl = new DataTable();
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

            List <LeadDto> leads = FillList();

            for (int i = 0; i < leads.Count; i++)
            {
                DataRow dr = tbl.NewRow();
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

            string connection = @"Server=DESKTOP-PMA057A;Database=CRM_System.DB;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "CRM_System.Db";
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