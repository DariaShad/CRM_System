using CRM.DataLayer;

namespace CRM_System.API;

public class LeadRegistrationRequest : LeadUpdateRequest
{
    public string Email { get; set; }
    public string Passport { get; set; }
    public string Password { get; set; }
}
