using CRM_System.BusinessLayer;

namespace CRM.DataLayer.Models;

public class LeadDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Passport { get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
    public bool IsDeleted { get; set; }
}
