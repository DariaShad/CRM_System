using CRM.DataLayer.Enums;

namespace CRM_System.API.Models.Requests
{
    public class UpdateAccountRequest
    {
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
