using IncredibleBackendContracts.Enums;

namespace CRM_System.DataLayer;

public class AccountDto
{
    public int Id { get; set; }
    public TradingCurrency Currency { get; set; }
    public AccountStatus Status { get; set; }
    public int LeadId { get; set; }
    public bool IsDeleted { get; set; }
}
