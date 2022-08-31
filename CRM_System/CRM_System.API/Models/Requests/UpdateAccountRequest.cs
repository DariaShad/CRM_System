using CRM_System.DataLayer;

namespace CRM_System.API.Models.Requests
{
    public class UpdateAccountRequest
    {
        public AccountStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
