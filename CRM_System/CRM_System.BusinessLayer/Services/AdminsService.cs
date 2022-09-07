using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer.Services
{
    public class AdminsService : IAdminsService
    {

        private readonly IAdminRepository _adminRepository;

        private readonly ILogger <AdminsService> _logger;

        public AdminsService (IAdminRepository adminRepository, ILogger<AdminsService> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }
        public async Task<AdminDto> GetAdminByEmail(string email)
        {
            _logger.LogInformation("Business layer: Database query for getting admin by email");
            var admin = await _adminRepository.GetAdminByEmail(email);

            if (admin is null)
                throw new NotFoundException($"Admin with email '{email}' was not found");

            else
                return admin;
        }

        public async Task <int> AddAdmin(AdminDto admin)
        {
            _logger.LogInformation("Business layer: Database query for adding admin");
            bool isUniqueEmail = await CheckEmailForUniqueness(admin.Email);
            if (!isUniqueEmail)
                throw new NotUniqueEmailException($"This email is registered already");

            else
                admin.Password = PasswordHash.HashPassword(admin.Password);

            return await _adminRepository.AddAdmin(admin);
        }
        private async Task<bool> CheckEmailForUniqueness(string email) => await _adminRepository.GetAdminByEmail(email) == null;
    }
}
