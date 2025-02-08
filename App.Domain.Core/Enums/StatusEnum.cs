﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Enums
{
	public enum StatusEnum
	{
		PendingClientConfirmation = 1,     
		PendingProviderConfirmation = 2,   
		Completed = 3,   
		Cancelled = 4    
	}
}
