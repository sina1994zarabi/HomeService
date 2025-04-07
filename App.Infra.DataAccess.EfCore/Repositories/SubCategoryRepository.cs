using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.SubCategoryDto;
using App.Domain.Core.Entities.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
	//public class SubCategoryRepository : ISubCategoryRepository
	//{
	//	private readonly AppDbContext _context;

 //       public SubCategoryRepository(AppDbContext context)
 //       {
	//		_context = context;
 //       }

 //       public async Task Add(CreateSubCategoryDto subcategory,CancellationToken cancellationToken)
	//	{
	//		var newSubCategory = new SubCategory
	//		{
	//			Title = subcategory.Title,
	//			CategoryId = subcategory.CategoryId,
	//			ImagePath = subcategory.ImagePath
	//		};
	//		await _context.Subcategories.AddAsync(newSubCategory,cancellationToken);
	//		await _context.SaveChangesAsync(cancellationToken);
	//	}

	//	public async Task Delete(int id,CancellationToken cancellationToken)
	//	{
	//		var subCategoryToDelete = await _context.Subcategories.FindAsync(id,cancellationToken);
	//		if (subCategoryToDelete != null)
	//		{
	//			_context.Subcategories.Remove(subCategoryToDelete);
	//			await _context.SaveChangesAsync(cancellationToken);
	//		}
	//	}

	//	public async Task<SubCategory> Get(int id,CancellationToken cancellationToken)
	//	{
	//		return await _context.Subcategories
	//			.Include(x => x.Category)
	//			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	//	}

	//	public async Task<List<SubCategory>> GetAll(CancellationToken cancellationToken)
	//	{
	//		return await _context.Subcategories.Include(x => x.Category).
	//			ToListAsync(cancellationToken);
	//	}


 //       public async Task Update(UpdateSubCategoryDto subcategory,CancellationToken cancellationToken)
	//	{
	//		var subcategoryToUpdate = await _context.Subcategories.FindAsync(subcategory.Id,cancellationToken);
	//		if (subcategoryToUpdate != null)
	//		{
	//			subcategoryToUpdate.Title = subcategory.Title;
	//			subcategoryToUpdate.CategoryId = subcategory.CategoryId;
	//			subcategoryToUpdate.ImagePath = subcategory.ImagePath;
	//			await _context.SaveChangesAsync(cancellationToken);
	//		}
	//	}
	//}
}
