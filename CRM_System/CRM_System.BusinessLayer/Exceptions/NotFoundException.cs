namespace CRM_System.BusinessLayer.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) 
    {
    }
}
