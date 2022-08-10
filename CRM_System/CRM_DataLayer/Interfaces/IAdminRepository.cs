using CRM.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Interfaces
{
    public interface IAdminRepository
    {
        public Task<AdminDto> GetAdminByEmail(string email);
        public Task<int> AddAdmin(AdminDto admin);
    }
}
