using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public interface IAdminsService
{
    public Task<AdminDto> GetAdminByEmail(string email);
    public Task<int> AddAdmin(AdminDto admin);
}
