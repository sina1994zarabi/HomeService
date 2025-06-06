﻿using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.DTOs.ExpertDto
{
	public class CreateExpertDto
	{
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int AppUserId { get; set; }
        public string? ImagePath { get; set; }
    }
}
