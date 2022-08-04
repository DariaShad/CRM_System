using CRM.DataLayer;

namespace CRM_System.BusinessLayer;

public class LeadModel : ClaimModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Phone { get; set; }
    public string Passport { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsDeleted { get; set; }
}
