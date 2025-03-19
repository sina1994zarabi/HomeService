using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
	public class UpdateExpertDto
	{
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string? ImagePath { get; set; }
		public IFormFile? Image { get; set; }
	}
}
