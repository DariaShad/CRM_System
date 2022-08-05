using CRM.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }

        public AccountStatus Status { get; set; }

        public int LeadId { get; set; }

        public bool IsDeleted { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AccountDto accountDto &&

                Id == accountDto.Id &&
                Currency == accountDto.Currency &&
                Status == accountDto.Status &&
                LeadId == accountDto.LeadId &&
                IsDeleted == accountDto.IsDeleted;

        }
    }
}
