using IncredibleBackendContracts.Enums;

namespace CRM_System.API.Models.Requests;

public class AddAccountRequest
{
    public TradingCurrency TradingCurrency { get; set; }
    public AccountStatus Status { get; set; }
    public int LeadId { get; set; }
}
