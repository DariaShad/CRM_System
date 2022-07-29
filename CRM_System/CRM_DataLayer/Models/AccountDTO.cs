using CRM.DataLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Models
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }

        public Status Status { get; set; }

        public int LeadId { get; set; }

        public bool IsDeleted { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is AccountDTO accountDTO &&

                Id == accountDTO.Id &&
                Currency == accountDTO.Currency &&
                Status == accountDTO.Status &&
                LeadId == accountDTO.LeadId &&
                IsDeleted == accountDTO.IsDeleted;

        }
    }
}
