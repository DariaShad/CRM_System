namespace CRM_System.DataLayer;

public class AdminDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
}
