using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestService(IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        public async Task Add(CreateServiceRequestDto serviceRequest, CancellationToken cancellationToken)
        {
            await _serviceRequestRepository.Add(serviceRequest, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceRequestRepository.Delete(id, cancellationToken);
        }

        public async Task<ServiceRequest> Get(int id, CancellationToken cancellationToken)
        {
            return await _serviceRequestRepository.Get(id, cancellationToken);
        }

        public async Task<List<ServiceRequest>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceRequestRepository.GetAll(cancellationToken);
        }

        public async Task Update(ServiceRequest serviceRequest, CancellationToken cancellationToken)
        {
            await _serviceRequestRepository.Update(serviceRequest, cancellationToken);
        }

        public async Task ChangeStatus(StatusEnum status,int id,CancellationToken cancellationToken)
        {
            await _serviceRequestRepository.ChangeStatus(status, id, cancellationToken);
        }

		public async Task MarkAsDone(int id, CancellationToken cancellationToken)
		{
			await _serviceRequestRepository.MarkAsDone(id,cancellationToken);
		}

        public async Task AcceptOffer(int serviceRequestId, int serviceOfferingId, CancellationToken cancellationToken)
        {
            await _serviceRequestRepository.AcceptOffer(serviceRequestId,serviceOfferingId,cancellationToken);
        }
    }
}
