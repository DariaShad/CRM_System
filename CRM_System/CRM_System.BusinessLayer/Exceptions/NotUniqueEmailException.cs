namespace CRM_System.BusinessLayer;

public class NotUniqueEmailException : Exception
{
    public NotUniqueEmailException(string message) : base(message) 
    { 
    }
}
