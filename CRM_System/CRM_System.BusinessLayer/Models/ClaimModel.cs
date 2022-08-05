using CRM.DataLayer;

namespace CRM_System.BusinessLayer;

public class ClaimModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
