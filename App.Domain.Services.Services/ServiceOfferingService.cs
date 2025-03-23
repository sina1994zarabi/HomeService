using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ServiceOfferingDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Services
{
    public class ServiceOfferingService : IServiceOfferingService
    {
        private readonly IServiceOfferingRepository _serviceOfferingRepository;

        public ServiceOfferingService(IServiceOfferingRepository serviceOfferingRepository)
        {
            _serviceOfferingRepository = serviceOfferingRepository;
        }

        public async Task<int> Add(CreateServiceOfferingDto serviceOffering, CancellationToken cancellationToken)
        {
            return await _serviceOfferingRepository.Add(serviceOffering, cancellationToken);
        }

        public async Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken)
        {
            await _serviceOfferingRepository.ChangeStatus(status,id,cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceOfferingRepository.Delete(id, cancellationToken);
        }

        public async Task<ServiceOffering> Get(int id, CancellationToken cancellationToken)
        {
            return await _serviceOfferingRepository.Get(id, cancellationToken);
        }

        public async Task<List<ServiceOffering>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceOfferingRepository.GetAll(cancellationToken);
        }

        public async Task Update(ServiceOffering serviceOffering, CancellationToken cancellationToken)
        {
            await _serviceOfferingRepository.Update(serviceOffering, cancellationToken);
        }
    }
}
