namespace CRM_System.BusinessLayer;

public class RegisteredEmailException : Exception
{
    public RegisteredEmailException(string message) : base(message) 
    { 
    }
}
