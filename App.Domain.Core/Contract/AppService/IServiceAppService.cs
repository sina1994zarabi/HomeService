using App.Domain.Core.DTOs.ServiceDto;
using App.Domain.Core.Entities.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.AppService
{
    public interface IServiceAppService
    {
        Task Create(CreateServiceDto createServiceDto,CancellationToken cancellationToken,IFormFile Image);
        Task<Service> GetById(int id, CancellationToken cancellationToken);
        Task<List<Service>> GetAll(CancellationToken cancellationToken);
        Task<List<Service>> GetServicesBySubCategory(int subCategoryId, CancellationToken cancellationToken);   
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(UpdateServiceDto updateServiceDto, CancellationToken cancellationToken,IFormFile Image);
    }
}
