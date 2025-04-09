using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
	public class UpdateExpertDto
	{
        public int Id { get; set; }
        public int AppUserId { get; set; }
		[Required(ErrorMessage = "نام خانوادگی نی تواند خالی باشد")]
        public string FullName { get; set; }
		[Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
		public string Username { get; set; }
		[Required(ErrorMessage = "ایمیل نمی تواند خالی باشد")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
		[Required(ErrorMessage = "شماره تلفن نمی تواند خالی باشد")]
		public string PhoneNumber { get; set; }
		public string? ImagePath { get; set; }
		public IFormFile? Image { get; set; }
	}
}
