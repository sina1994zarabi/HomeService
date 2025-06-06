﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.Entities.BaseEntity;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;

namespace App.Domain.Core.Entities.User
{
    public class Client
    {
        #region Properties
        public int Id { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int AppUserId { get; set; }
        #endregion

        #region Navigation properties
        public AppUser AppUser { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        #endregion
    }
}
