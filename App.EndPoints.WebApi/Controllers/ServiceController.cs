using App.Domain.Core.Contract.AppService;
using App.Domain.Services.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;
        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly IServiceAppService _serviceAppService;

        public ServiceController(ICategoryAppService categoryAppService,
                                 ISubCategoryAppService subCategoryAppService,
                                 IServiceAppService serviceAppService)
        {
            _categoryAppService = categoryAppService;
            _subCategoryAppService = subCategoryAppService;
            _serviceAppService = serviceAppService;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryAppService.GetAll(default);
            return Ok(categories);
        }

        [HttpGet("SubCategories")]
        public async Task<IActionResult> SubCategories()
        {
            var subCategories = await _subCategoryAppService.GetAll(default);
            return Ok(subCategories);
        }

        [HttpGet("Services")]
        public async Task<IActionResult> Sercvices()
        {
            var services = await _serviceAppService.GetAll(default);
            return Ok(services);
        }
    }
}
