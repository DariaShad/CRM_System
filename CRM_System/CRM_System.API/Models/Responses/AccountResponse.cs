﻿using CRM.DataLayer;
using CRM.DataLayer.Enums;

namespace CRM_System.API.Models.Responses
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }

        public Status Status { get; set; }

        public int LeadId { get; set; }

        public bool IsDeleted { get; set; }
    }
}