namespace CRM_System.DataLayer;

public interface IAdminRepository
{
    public Task<AdminDto> GetAdminByEmail(string email);
    public Task<int> AddAdmin(AdminDto admin);
}
