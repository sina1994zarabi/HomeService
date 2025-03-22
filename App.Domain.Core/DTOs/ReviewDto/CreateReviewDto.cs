using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ReviewDto
{
    public class CreateReviewDto
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int ClientId { get; set; }
        public int ServiceOfferingId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
