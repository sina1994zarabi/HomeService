using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ServiceOfferingDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ServiceOfferingAppService : IServiceOfferingAppService
    {
        private readonly IServiceOfferingService _serviceOfferingService;

        public ServiceOfferingAppService(IServiceOfferingService serviceOfferingService)
        {
           _serviceOfferingService = serviceOfferingService;
        }

        public async Task ChangeStatus(int id, StatusEnum status, CancellationToken cancellationToken)
        {
            await _serviceOfferingService.ChangeStatus(status,id,cancellationToken);
        }

        public async Task<int> Create(CreateServiceOfferingDto createServiceOfferingDto, CancellationToken cancellationToken)
        {
            return await _serviceOfferingService.Add(createServiceOfferingDto,cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceOfferingService.Delete(id,cancellationToken);
        }

        public async Task<List<ServiceOffering>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceOfferingService.GetAll(cancellationToken);
        }

        public async Task<ServiceOffering> GetById(int id, CancellationToken cancellationToken)
        {
            return await _serviceOfferingService.Get(id,cancellationToken);
        }

        public async Task Update(ServiceOffering serviceOffering, CancellationToken cancellation)
        {
            await _serviceOfferingService.Update(serviceOffering,cancellation);
        }
    }
}
