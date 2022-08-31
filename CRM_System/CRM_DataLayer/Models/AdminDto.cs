namespace CRM.DataLayer.Models;

public class AdminDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
}
