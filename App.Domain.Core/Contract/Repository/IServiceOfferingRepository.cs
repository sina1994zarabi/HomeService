using App.Domain.Core.DTOs.ServiceOfferingDto;
using App.Domain.Core.Entities;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.Repository
{
	public interface IServiceOfferingRepository
	{
		Task<int> Add(CreateServiceOfferingDto serviceOffering,CancellationToken cancellationToken);
		Task<ServiceOffering> Get(int id,CancellationToken cancellationToken);
		Task<List<ServiceOffering>> GetAll(CancellationToken cancellationToken);
		Task Delete(int id, CancellationToken cancellationToken);
		Task Update(ServiceOffering serviceOffering, CancellationToken cancellationToken);
		Task ChangeStatus(StatusEnum status, int id, CancellationToken cancellationToken);
	}
}
