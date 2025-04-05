using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
    public class ExpertProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal AccountBalance { get; set; }
        public bool IsActive { get; set; }
    }
}
