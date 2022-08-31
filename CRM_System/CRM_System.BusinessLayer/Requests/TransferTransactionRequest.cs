using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class TransferTransactionRequest
{
    public int SenderAccountId { get; set; }
    public int RecipientAccountId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}
