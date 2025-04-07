using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.CategoryDto;
using App.Domain.Core.Entities.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _context;


        public CategoryRepository(AppDbContext context)
        {
			_context = context;
        }

        public async Task Add(CreateCategoryDto category,CancellationToken cancellationToken)
		{
			var newCategory = new Category
			{
				Title = category.Title,
				Image = category.ImagePath
			};
			await _context.Categories.AddAsync(newCategory,cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task Delete(int id,CancellationToken cancellationToken)
		{
			var CategoryToDelete = await _context.Categories.FindAsync(id, cancellationToken);
			if (CategoryToDelete != null)
			{
				_context.Categories.Remove(CategoryToDelete);
				await _context.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task<Category> Get(int id,CancellationToken cancellationToken)
		{
			return await _context.Categories.FindAsync(id, cancellationToken);
		}

		public async Task<List<Category>> GetAll(CancellationToken cancellationToken)
		{
			return await _context.Categories
								 .AsNoTracking()
								 .Include(x => x.Subcategories)
								 .ToListAsync(cancellationToken);
		}

		public async Task Update(UpdateCategoryDto category,CancellationToken cancellationToken)
		{
			var categoryToUpdate = await _context.Categories.FindAsync(category.Id, cancellationToken);
			if (categoryToUpdate != null)
			{
				categoryToUpdate.Title = category.Title;
				categoryToUpdate.Image = category.ImagePath;
				await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
