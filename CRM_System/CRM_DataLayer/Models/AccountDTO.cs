namespace CRM.DataLayer.Models;

public class AccountDto
{
    public int Id { get; set; }
    public Currency Currency { get; set; }
    public AccountStatus Status { get; set; }
    public int LeadId { get; set; }
    public bool IsDeleted { get; set; }
}
