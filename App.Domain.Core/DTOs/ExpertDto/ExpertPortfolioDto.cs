using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
	public class ExpertPortfolioDto
	{
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public List<Service> Skills { get; set; }
        public double AverageRating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
