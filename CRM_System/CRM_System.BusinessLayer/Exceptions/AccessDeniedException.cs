namespace CRM_System.BusinessLayer;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(string message) : base(message)
    {
    }
}
