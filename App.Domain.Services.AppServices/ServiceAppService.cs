using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class ServiceAppService : IServiceAppService
    {

        private readonly IServiceService _serviceService;
        private readonly IUtilityService _utilityService;

        public ServiceAppService(IServiceService serviceService, IUtilityService utilityService)
        {
            _serviceService = serviceService;
            _utilityService = utilityService;
        }

        public async Task Create(CreateServiceDto createServiceDto, CancellationToken cancellationToken,IFormFile Image)
        {
            var ImagePath = await _utilityService.UploadImage(Image);
            createServiceDto.ImagePath = ImagePath;
            await _serviceService.Add(createServiceDto,cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _serviceService.Delete(id,cancellationToken);
        }

        public async Task<List<Service>> GetAll(CancellationToken cancellationToken)
        {
            return await _serviceService.GetAll(cancellationToken);
        }

        public async Task<Service> GetById(int id, CancellationToken cancellationToken)
        {
            return await _serviceService.Get(id,cancellationToken);
        }

        public async Task<List<Service>> GetServicesBySubCategory(int subCategoryId, CancellationToken cancellationToken)
        {
            var services = await _serviceService.GetAll(cancellationToken);
            return services.Where(x => x.SubCategoryId == subCategoryId).ToList();
        }

        public async Task Update(UpdateServiceDto updateServiceDto, CancellationToken cancellationToken,IFormFile Image)
        {
            var ImagePath = await _utilityService.UploadImage(Image);
            updateServiceDto.ImagePath = ImagePath;
            await _serviceService.Update(updateServiceDto,cancellationToken);
        }
    }
}
