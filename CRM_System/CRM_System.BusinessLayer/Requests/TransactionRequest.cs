using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class TransactionRequest
{
    public int AccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}
