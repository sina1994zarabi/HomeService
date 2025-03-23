using App.Domain.Core.DTOs.ServiceOfferingDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IServiceOfferingAppService
    {
        Task<int> Create(CreateServiceOfferingDto createServiceOfferingDto, CancellationToken cancellationToken);
        Task<ServiceOffering> GetById(int id, CancellationToken cancellationToken);
        Task<List<ServiceOffering>> GetAll(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(ServiceOffering serviceOffering, CancellationToken cancellation);
        Task ChangeStatus(int id,StatusEnum status,CancellationToken cancellationToken);
    }
}
