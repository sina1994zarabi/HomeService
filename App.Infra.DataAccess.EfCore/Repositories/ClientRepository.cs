using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
    public class ClientRepository : IClientRepository
	{
		private readonly AppDbContext _context;
		private readonly IUtilityService _utilityService;

        public ClientRepository(AppDbContext context,IUtilityService utilityService)
        {
			_context = context;
			_utilityService = utilityService;
        }

        public async Task Add(CreateClientDto client,CancellationToken cancellation)
		{
			var newClient = new Client
			{
				FullName = client.FullName,
				//Email = client.Email,
				//UserName = client.Username,
				//PasswordHash = client.Password,
				PhoneNumber = client.PhoneNumber,
				Gender = client.Gender,
				AppUserId = client.AppUserId
			};
			await _context.Clients.AddAsync(newClient,cancellation);
			await _context.SaveChangesAsync(cancellation);
		}

        public async Task ChangeAccountBalance(int clientId,decimal amount, CancellationToken cancellation)
        {
            var Client = await _context.Clients.FindAsync(clientId,cancellation);
			Client.AppUser.AccountBalance += amount;
			await _context.SaveChangesAsync(cancellation);
        }

        public async Task Delete(int id,CancellationToken cancellation)
		{
			var client = await _context.Clients.FindAsync(id, cancellation);
			if (client != null)
			{
				_context.Clients.Remove(client);
				await _context.SaveChangesAsync(cancellation);
			}
		}

		public async Task<Client> Get(int id,CancellationToken cancellation)
		{
			return await _context.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellation);
		}

		public async Task<List<Client>> GetAll(CancellationToken cancellation)
		{
			return await _context.Clients.
								  Include(c => c.AppUser).
								  ToListAsync(cancellation);
		}

        public async Task<Client> GetClientInfo(int id, CancellationToken cancellation)
        {
            return await _context.Clients
				.Include(x => x.AppUser)
				.Include(x => x.ServiceRequests)
				.FirstAsync(x => x.Id == id);
        }

		public async Task MakeTransaction(int clientId, decimal amount, CancellationToken cancellationToken)
		{
			var client = await _context.
								Clients.
								Include(x => x.AppUser).
								FirstOrDefaultAsync(x => x.Id == clientId);
			var admin = await _context.Admins
									.Include(x => x.AppUser)
									.FirstOrDefaultAsync();
			client.AppUser.AccountBalance -= amount;
			admin.AppUser.AccountBalance += amount;
			await _context.SaveChangesAsync(default);
		}

		public async Task Update(UpdateClientprofileDto client,CancellationToken cancellation,IFormFile Image)
		{
		    var clientToUpdate = await _context.Clients
				.Include(c => c.AppUser)
				.FirstOrDefaultAsync(c => c.Id == client.Id, cancellation);
			var ImagePath = await _utilityService.UploadImage(Image);
			if (clientToUpdate != null)
			{
				clientToUpdate.AppUser.Email = client.Email;
				clientToUpdate.PhoneNumber  = client.PhoneNumber;
				clientToUpdate.AppUser.PhoneNumber  = client.PhoneNumber;
				clientToUpdate.FullName = client.FullName;
				clientToUpdate.AppUser.FullName = client.FullName;
				clientToUpdate.AppUser.UserName = client.Username;
				clientToUpdate.AppUser.ProfilePicture = ImagePath;
				await _context.SaveChangesAsync(cancellation);
			}
		}

        public async Task UpdateBalance(int id, decimal amount, CancellationToken cancellation)
        {
            var client = await _context.Clients.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
			client.AppUser.AccountBalance += amount;
			await _context.SaveChangesAsync(cancellation);
        }
    }
}
