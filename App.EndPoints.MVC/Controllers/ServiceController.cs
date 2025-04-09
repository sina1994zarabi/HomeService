using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Entities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.MVC.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceAppService _serviceAppService;

        public ServiceController(
            ILogger<ServiceController> logger,
            IMemoryCache memoryCache,
            IServiceAppService serviceAppService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _serviceAppService = serviceAppService;
        }

        
        public async Task<IActionResult> Index()
        {
            string servicesCacheKey = "ServicesCachKey";
            if (!_memoryCache.TryGetValue(servicesCacheKey, out List<Service> model))
            {
                model = await _serviceAppService.GetAll(default);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
                _memoryCache.Set(servicesCacheKey, model, cacheEntryOptions);
            }
            return View(model);
        }

        
        public async Task<IActionResult> Details(int Id)
        {
            var model = await _serviceAppService.GetById(Id, default);
            return View(model);
        }


    }
}
