using CRM.DataLayer;

namespace CRM_System.API;

public class LeadAllInfoResponse : LeadMainInfoResponse
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Passport { get; set; }
    public string? Address { get; set; }
    public Role Role { get; set; }
    public DateTime RegistrationDate { get; set; }
}
