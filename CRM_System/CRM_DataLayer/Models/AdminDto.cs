using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Models
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AdminDto adminDto &&

                Id == adminDto.Id &&
                Role == adminDto.Role &&
                Email == adminDto.Email &&
                Password == adminDto.Password &&
                IsDeleted == adminDto.IsDeleted;

        }
    }
}
