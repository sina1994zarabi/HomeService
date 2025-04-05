using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.DataAccess.EfCore.Repositories
{
	public class ServiceRequestRepository : IServiceRequestRepository
	{
		private readonly AppDbContext _context;

        public ServiceRequestRepository(AppDbContext context)
        {
			_context = context;
        }

        public async Task AcceptOffer(int serviceRequestId, int serviceOfferingId, CancellationToken cancellationToken)
        {
			var request = await _context.ServiceRequests
										.Include(r => r.ServiceOfferings)
										.FirstOrDefaultAsync(r => r.Id == serviceRequestId);
            var selectedOffer = request.ServiceOfferings.FirstOrDefault(o => o.Id == serviceOfferingId);
            request.Status = StatusEnum.PendingProviderConfirmation;
            selectedOffer.Status = StatusEnum.PendingProviderConfirmation;
            foreach (var offer in request.ServiceOfferings)
            {
                if (offer.Id != serviceOfferingId)
                {
                    offer.Status = StatusEnum.Cancelled;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task Add(CreateServiceRequestDto serviceRequest,CancellationToken cancellationToken)
		{
			var newServiceRequest = new ServiceRequest { 
				Title = serviceRequest.Title,
				Description = serviceRequest.Description,
				ServiceId = serviceRequest.ServiceId,
				ClientId  = serviceRequest.ClientId,
				BookingDate = serviceRequest.BookingDate
			};
			await _context.ServiceRequests.AddAsync(newServiceRequest,cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken)
		{
			var serviceRequest = await _context.ServiceRequests.FindAsync(id,cancellationToken);
			var serviceOffering = await _context.ServiceOfferings.FirstOrDefaultAsync(x => x.ServiceRequestId == id,cancellationToken);
			serviceRequest.Status = status;
			serviceOffering.Status = status;
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task Delete(int id,CancellationToken cancellationToken)
		{
			var serviceRequestToDelete = await _context.ServiceRequests.FindAsync(id,cancellationToken);
			if (serviceRequestToDelete != null)
			{
				_context.ServiceRequests.Remove(serviceRequestToDelete);
				await _context.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task<ServiceRequest> Get(int id,CancellationToken cancellationToken)
		{
			return await _context.ServiceRequests
								 .Include(sr => sr.Client)
										.ThenInclude(c => c.AppUser)
								 .Include(sr => sr.Service)
								 .Include(sr => sr.Images)
								 .Include(sr => sr.ServiceOfferings)
									    .ThenInclude(so => so.Expert)
								 .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
		}

		public async Task<List<ServiceRequest>> GetAll(CancellationToken cancellationToken)
		{
			return await _context.ServiceRequests
								  .Include(x => x.Client)
								  .Include(x => x.Service)
								  .Include(x => x.ServiceOfferings)
											.ThenInclude(x => x.Expert)
								  .Include(x => x.Images)
								  .ToListAsync(cancellationToken);
		}

        public async Task<ServiceOffering> GetApprovedOffer(int id, CancellationToken cancellationToken)
        {
			var requests = await _context.
								 ServiceRequests.
								 Include(x => x.ServiceOfferings).
								 ToListAsync(cancellationToken);
            var serviceOffering = requests.SelectMany(r => r.ServiceOfferings)
									.FirstOrDefault(s => s.Status == StatusEnum.AwaitingAdminApprovalForTransaction);
			return serviceOffering;
        }

        public async Task Update(ServiceRequest serviceRequest,CancellationToken cancellationToken)
		{
			var serviceRequestToEdit = await _context.ServiceRequests.FindAsync(serviceRequest.Id,cancellationToken);
			if (serviceRequestToEdit != null)
			{
				serviceRequestToEdit.Title = serviceRequest.Title;
				serviceRequestToEdit.Status = serviceRequest.Status;
				serviceRequestToEdit.BookingDate = serviceRequest.BookingDate;
				serviceRequestToEdit.Description = serviceRequest.Description;
				serviceRequestToEdit.IsCompleted = serviceRequest.IsCompleted;
				await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
