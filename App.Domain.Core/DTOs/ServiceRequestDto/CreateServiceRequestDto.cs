using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ServiceRequestDto
{
    public class CreateServiceRequestDto
    {
        [Required(ErrorMessage = "عنوان درخواست نمی تواند خالی باشد")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
