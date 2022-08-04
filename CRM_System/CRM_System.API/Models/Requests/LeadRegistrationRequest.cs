using CRM.DataLayer;

namespace CRM_System.API;

public class LeadRegistrationRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Passport { get; set; }
    public City City { get; set; }
    public string? Address { get; set; }
    public string? Password { get; set; }
}
