using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ServiceRequestDto
{
    public class ChangeStatusDto
    {
        public int ServiceRequestId { get; set; }
        public StatusEnum Status { get; set; }
        public StatusEnum CurrentStatus { get; set; }
    }
}
