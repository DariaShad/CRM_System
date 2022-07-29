using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.StoredProcedure
{
    public class AccountStoredProcedure
    {
        public const string Account_Add = "Account_Add";
        public const string Account_GetAll = "Account_GetAll";
        public const string Account_GetById = "Account_GetById";
        public const string Account_Delete = "Account_Delete";
        public const string Account_Update = "Account_Update";
        public const string Account_GetAllAccountsByLeadId = "Account_GetAllAccountsByLeadId";
    }
}
