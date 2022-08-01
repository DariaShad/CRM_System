using CRM.DataLayer;

namespace CRM_System.BusinessLayer;

public class UserModel
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public string Password { get; set; }
    public bool isDeleted { get; set; }
}
