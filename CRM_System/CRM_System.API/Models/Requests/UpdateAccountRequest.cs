using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;

namespace CRM_System.API.Models.Requests
{
    public class UpdateAccountRequest
    {
        public AccountStatus Status { get; set; }
    }
}
