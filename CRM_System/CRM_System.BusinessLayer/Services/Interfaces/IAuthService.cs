using CRM_System.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ClaimModel> Login(LoginRequest loginRequest);
        string GetToken(ClaimModel claimModel);
    }
}
