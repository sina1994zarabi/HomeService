using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
	//public class ServiceRepository : IServiceRepository
	//{
	//	private readonly AppDbContext _context;


 //       public ServiceRepository(AppDbContext context)
 //       {
	//		_context = context;
 //       }

 //       public async Task Add(CreateServiceDto service,CancellationToken cancellationToken)
	//	{
	//		var newService = new Service
	//		{
	//			Title = service.Title,
	//			Description = service.Description,
	//			Price = service.Price,
	//			SubCategoryId = service.SubCategoryId,
	//		};
	//		await _context.Services.AddAsync(newService,cancellationToken);
	//		await _context.SaveChangesAsync(cancellationToken);
	//	}

	//	public async Task Delete(int id,CancellationToken cancellationToken)
	//	{
	//		var serviceToDelete = await _context.Services.FindAsync(id, cancellationToken);
	//		if (serviceToDelete != null)
	//		{
	//			_context.Services.Remove(serviceToDelete);
	//			await _context.SaveChangesAsync(cancellationToken);
	//		}
	//	}

	//	public async Task<Service> Get(int id,CancellationToken cancellationToken)
	//	{
	//		return await _context.Services.
	//			Include(x => x.Subcategory).
	//			FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	//	}

	//	public async Task<List<Service>> GetAll(CancellationToken cancellationToken)
	//	{
	//		return await _context.Services
	//				.Include(x => x.Subcategory).				
	//			    ToListAsync(cancellationToken);
	//	}

	//	public async Task Update(UpdateServiceDto service,CancellationToken cancellationToken)
	//	{
	//		var serviceToEdit = await _context.Services.FindAsync(service.Id, cancellationToken);
	//		if (serviceToEdit != null)
	//		{
	//			serviceToEdit.Title = service.Title;
	//			serviceToEdit.Description = service.Description;
	//			serviceToEdit.Price = service.Price;
	//			serviceToEdit.SubCategoryId = service.SubCategoryId;
	//			serviceToEdit.Image = service.ImagePath;
	//			await _context.SaveChangesAsync(cancellationToken);
	//		}
	//	}
	//}
}
