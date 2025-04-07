using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
	//public class CityRepository : ICityRepository
	//{
	//	private readonly AppDbContext _context;

 //       public CityRepository(AppDbContext context)
 //       {
	//		_context = context;
 //       }

 //       public async Task Add(City city,CancellationToken cancellationToken)
	//	{
	//		_context.Cities.Add(city);
	//		await _context.SaveChangesAsync(cancellationToken);
	//	}

	//	public async Task Delete(int id,CancellationToken cancellationToken)
	//	{
	//		var cityToDelete = await _context.Cities.FindAsync(id,cancellationToken);
	//		if (cityToDelete != null)
	//		{
	//			_context.Cities.Remove(cityToDelete);
	//			await _context.SaveChangesAsync(cancellationToken);
	//		}
	//	}

	//	public async Task<City> Get(int id,CancellationToken cancellationToken)
	//	{
	//		return await _context.Cities.FindAsync(id, cancellationToken);
	//	}

	//	public async Task<List<City>> GetAll(CancellationToken cancellationToken)
	//	{
	//		return await _context.Cities.ToListAsync(cancellationToken);
	//	}

	//	public async Task Update(City city,CancellationToken cancellation)
	//	{
	//		var cityToEdit = await _context.Cities.FindAsync(city.Id,cancellation);
	//		if ( cityToEdit != null )
	//		{
	//			cityToEdit.Name = city.Name;
	//			await _context.SaveChangesAsync(cancellation);
	//		}
	//	}
	//}
}
