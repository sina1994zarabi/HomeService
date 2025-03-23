using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IExpertAppService
    {
        Task Create(CreateExpertDto createExpertDto,CancellationToken cancellationToken);
        Task<Expert> GetById(int id, CancellationToken cancellationToken);
        Task<Expert> GetExpertByUserId(int id,CancellationToken cancellationToken);
        Task<List<Expert>> GetAll(CancellationToken cancellationToken);
        Task<Expert> GetExpertInfo(int id, CancellationToken cancellation);
        Task<List<Review>> GetExpertReview(int Id, CancellationToken cancellationToken);
        Task<List<GetServiceDto>> GetExpertSkills(int userId, CancellationToken cancellationToken);
        Task<List<ServiceRequest>> GetServiceRequests(int Id, CancellationToken cancellationToken);
        Task<List<ServiceOffering>> GetServiceOfferings(int Id, CancellationToken cancellationToken);
        Task RemoveSkills(int id,Service service,CancellationToken cancellationToken);
        Task UpdateSkills(int id, Service service, CancellationToken cancellation);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(UpdateExpertDto updateExpertDto, CancellationToken cancellationToken);
        Task ChangeStatus(int id, CancellationToken cancellationToken);
    }
}
