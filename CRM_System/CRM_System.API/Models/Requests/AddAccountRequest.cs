using CRM_System.DataLayer;

namespace CRM_System.API.Models.Requests;

public class AddAccountRequest
{
    public Currency Currency { get; set; }
    public AccountStatus Status { get; set; }
    public int LeadId { get; set; }
}
