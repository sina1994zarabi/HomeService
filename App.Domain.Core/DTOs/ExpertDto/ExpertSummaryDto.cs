using App.Domain.Core.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
	public class ExpertSummaryDto
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public List<Service> Skills { get; set; }
		public string ProfilePicture { get; set; }
		public double AvarageRatings { get; set; }
	}
}
