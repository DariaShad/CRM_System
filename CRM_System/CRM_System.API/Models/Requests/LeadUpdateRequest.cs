using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;

namespace CRM_System.API;

public class LeadUpdateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Phone { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
}
