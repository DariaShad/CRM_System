using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Services
{
    public class AdminsService : IAdminsService
    {

        private readonly IAdminRepository _adminRepository;

        public AdminsService (IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<AdminDto> GetAdminByEmail(string email)
        {
            var admin = await _adminRepository.GetAdminByEmail(email);

            if (admin is null)
                throw new NotFoundException($"Admin with {email} was not found");

            else
                return admin;
        }

        public async Task <int> AddAdmin(AdminDto admin)
        {
            bool isUniqueEmail = await CheckEmailForUniqueness(admin.Email);
            if (!isUniqueEmail)
                throw new RegisteredEmailException($"This email is registered already");

            else
                admin.Password = PasswordHash.HashPassword(admin.Password);
            admin.Role = Role.Regular;

            return await _adminRepository.AddAdmin(admin);
        }
        private async Task<bool> CheckEmailForUniqueness(string email) => await _adminRepository.GetAdminByEmail(email) == null;
    }
}
