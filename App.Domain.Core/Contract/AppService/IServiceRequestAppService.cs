using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IServiceRequestAppService
    {
        Task Create(CreateServiceRequestDto createServiceRequestDto, CancellationToken cancellationToken);
        Task<ServiceRequest> GetById(int id, CancellationToken CancellationToken);
        Task<ServiceOffering> GetApprovedOffer(int id, CancellationToken cancellationToken);
        Task<List<ServiceRequest>> GetAll(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(ServiceRequest serviceRequest, CancellationToken cancellationToken);
        Task AcceptOffer(int requestId, int offerId,CancellationToken cancellation);
        Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken);
    }
}
