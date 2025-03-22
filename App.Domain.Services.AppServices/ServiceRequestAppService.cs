using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ServiceRequestAppService : IServiceRequestAppService
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestAppService(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        public async Task AcceptOffer(int requestId, int offerId,CancellationToken cancellationToken)
        {
            await _serviceRequestService.AcceptOffer(requestId,offerId,cancellationToken);
        }

        public async Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken)
		{
			await _serviceRequestService.ChangeStatus(status,id,cancellationToken);
		}

		public async Task Create(CreateServiceRequestDto createServiceRequestDto, CancellationToken cancellationToken)
        {
            await _serviceRequestService.Add(createServiceRequestDto,cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceRequestService.Delete(id,cancellationToken);
        }

        public async Task<List<ServiceRequest>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceRequestService.GetAll(cancellationToken);
        }

        public async Task<ServiceRequest> GetById(int id, CancellationToken CancellationToken)
        {
            return await _serviceRequestService.Get(id,CancellationToken);
        }

		public async Task MarkAsDone(int id, CancellationToken cancellationToken)
		{
			await _serviceRequestService.MarkAsDone(id,cancellationToken);
		}

		public async Task Update(ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            await _serviceRequestService.Update(serviceRequest,cancellationToken);
        }
    }
}
