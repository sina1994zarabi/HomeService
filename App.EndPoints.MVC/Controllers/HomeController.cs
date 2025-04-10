using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using App.Domain.Services.AppServices;
using App.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.EndPoints.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICategoryAppService _categoryAppService;
		private readonly IServiceAppService _serviceAppService;
		private readonly ISubCategoryAppService _subCategoryAppService;
		private readonly IExpertAppService _expertAppService;
		private readonly IReviewAppService _reviewAppService;
		private readonly IMemoryCache _memoryCache;

		public HomeController(ILogger<HomeController> logger,
            ICategoryAppService categoryAppService,
			IServiceAppService serviceAppService,
			ISubCategoryAppService subCategoryAppService,
			IMemoryCache memoryCache,
			IExpertAppService expertAppService,
			IReviewAppService reviewAppService)
		{
			_logger = logger;
			_categoryAppService = categoryAppService;
			_serviceAppService = serviceAppService;
			_subCategoryAppService = subCategoryAppService;
			_memoryCache = memoryCache;
			_expertAppService = expertAppService;
			_reviewAppService = reviewAppService;
		}

		public async Task<IActionResult> Index()
		{
            string categoriesCacheKey = "CategoriesCacheKey";
            string ServiceCacheKey = "ServiceCacheKey";
            if (!_memoryCache.TryGetValue(categoriesCacheKey, out List<Category> categories))
            {
                categories = await _categoryAppService.GetAll(default);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(categoriesCacheKey, categories, cacheEntryOptions);
            }
			if (!_memoryCache.TryGetValue(ServiceCacheKey, out List<Service> services))
			{
                services = await _serviceAppService.GetAll(default);
				var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
				_memoryCache.Set(ServiceCacheKey, services, cacheEntryOptions);
			}
			ViewBag.Services = services.Take(3);
			return View(categories);
        }

        public async Task<IActionResult> Services(int Id)
        {
			var model = await _serviceAppService.GetServicesBySubCategory(Id, default);
			return View(model);
		}

		public async Task<IActionResult> Categories()
		{
            string categoriesCacheKey = "CategoriesCacheKey";
            if (!_memoryCache.TryGetValue(categoriesCacheKey, out List<Category> categories))
            {
                categories = await _categoryAppService.GetAll(default);
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(categoriesCacheKey, categories, cacheEntryOptions);
            }
            var model = categories;
            return View(model);
        }

        public async Task<IActionResult> Subcategories(int Id)
        {
            var model = await _subCategoryAppService.GetAllSubCategoriesByCategoryId(Id,default);
            return View(model);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error()
		//{
		//	return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}
	}
}