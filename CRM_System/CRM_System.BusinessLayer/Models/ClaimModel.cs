using CRM.DataLayer;

namespace CRM_System.BusinessLayer;

public class ClaimModel
{
    public int Id { get; set; }
    // remove Email
    public Role Role { get; set; }
}
