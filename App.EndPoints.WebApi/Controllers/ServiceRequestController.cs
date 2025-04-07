using App.Domain.Core.Contract.AppService;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestAppService _serviceRequestAppService;

        public ServiceRequestController(IServiceRequestAppService serviceRequestAppService)
        {
            _serviceRequestAppService = serviceRequestAppService;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<IActionResult> GetServiceRequests()
        {
            var serviceRequests = await _serviceRequestAppService.GetAll(default);
            return Ok(serviceRequests);
        }
    }
}
