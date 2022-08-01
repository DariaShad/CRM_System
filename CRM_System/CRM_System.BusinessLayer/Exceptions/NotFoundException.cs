namespace CRM_System.BusinessLayer;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) 
    {
    }
}
