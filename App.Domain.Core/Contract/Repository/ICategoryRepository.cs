﻿using App.Domain.Core.DTOs.CategoryDto;
using App.Domain.Core.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.Repository
{
	public interface ICategoryRepository
	{
		Task Add(CreateCategoryDto category,CancellationToken cancellationToken);
		Task<Category> Get(int id,CancellationToken cancellationToken);
		Task<List<Category>> GetAll(CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Update(UpdateCategoryDto category, CancellationToken cancellationToken);
	}
}
