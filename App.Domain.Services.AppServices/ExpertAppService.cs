using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ExpertAppService : IExpertAppService
    {
        private readonly IExpertService _expertService;
        private readonly IReviewService _reviewService;
        private readonly IUtilityService _utilityService;

        public ExpertAppService(IExpertService expertService,
                                IReviewService reviewService,
                                IUtilityService utilityService)
        {
            _expertService = expertService;
            _reviewService = reviewService;
            _utilityService = utilityService;
        }

        public async Task ChangeStatus(int id, CancellationToken cancellationToken)
        {
            await _expertService.ChangeStatus(id, cancellationToken);
        }

        public async Task Create(CreateExpertDto createExpertDto, CancellationToken cancellationToken)
        {
            await _expertService.Add(createExpertDto,cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _expertService.Delete(id,cancellationToken);
        }

        public async Task<List<Expert>> GetAll(CancellationToken cancellationToken)
        {
            return await _expertService.GetAll(cancellationToken);
        }

        public async Task<Expert> GetById(int id, CancellationToken cancellationToken)
        {
            return await _expertService.Get(id,cancellationToken);
        }

        public async Task<Expert> GetExpertByUserId(int userId, CancellationToken cancellationToken)
        {
            var Experts = await _expertService.GetAll(default);
            return (Expert)Experts.Where(e => e.AppUserId == userId).FirstOrDefault();
        }

        public async Task<Expert> GetExpertInfo(int id, CancellationToken cancellationToken)
        {
            return await _expertService.GetExpertInfo(id,cancellationToken);
        }

        public async Task<List<Review>> GetExpertReview(int Id, CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAll(default);
            return reviews.Where(x => x.ServiceOffering.ExpertId == Id).ToList();
        }

        public async Task<List<GetServiceDto>> GetExpertSkills(int userId, CancellationToken cancellationToken)
        {
            var expert = await GetExpertByUserId(userId, default);
            var services = expert.Services;
            var skills = new List<GetServiceDto>();
            foreach (var service in services)
            {
                skills.Add(new GetServiceDto
                {
                    Id = service.Id,
                    Title = service.Title
                });
            }
            return skills;
        }

        public async Task RemoveSkills(int id, Service service, CancellationToken cancellationToken)
        {
            await _expertService.RemoveSkill(id,service,cancellationToken);
        }

        public async Task Update(UpdateExpertDto updateExpertDto, CancellationToken cancellationToken)
        {
            var imagePath = await _utilityService.UploadImage(updateExpertDto.Image);
            updateExpertDto.ImagePath = imagePath;
            await _expertService.Update(updateExpertDto,cancellationToken);
        }

        public async Task UpdateSkills(int id, Service service, CancellationToken cancellation)
        {
            await _expertService.UpdateSkills(id,service,cancellation);
        }
    }
}
