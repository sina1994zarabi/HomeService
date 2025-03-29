using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.Repository
{
	public interface IServiceRequestRepository
	{
		Task Add(CreateServiceRequestDto serviceRequest,CancellationToken cancellationToken);
		Task<ServiceRequest> Get(int id,CancellationToken cancellationToken);
		Task<List<ServiceRequest>> GetAll(CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Update(ServiceRequest serviceRequest, CancellationToken cancellationToken);
		Task AcceptOffer(int serviceRequestId, int serviceOfferingId, CancellationToken cancellationToken);
		Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken);
	}
}
