using CRM.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Services.Interfaces
{
    public interface IAdminsService
    {
        public Task<AdminDto> GetAdminByEmail(string email);
        public Task<int> AddAdmin(AdminDto admin);
    }
}
