using CRM.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Services
{
    public class AccessService
    {
        public static void CheckAccessForLeadAndManager(int id, ClaimModel claims)
        {
            if (claims is not null && claims.Id != id &&
                claims.Role != Role.Admin)
            {
                throw new AccessDeniedException($"Access denied");
            }
        }
    }
}
