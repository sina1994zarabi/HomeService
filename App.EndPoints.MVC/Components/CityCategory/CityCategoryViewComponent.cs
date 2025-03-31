using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.Entities.BaseEntity;
using App.Domain.Core.Entities.Services;
using App.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.MVC.Components.CityCategory
{
    public class CityCategoryViewComponent : ViewComponent
    {
        private readonly ICityService _cityService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IMemoryCache _cache;

        public CityCategoryViewComponent(ICityService cityService,
                                         ICategoryAppService categoryAppService,
                                         IMemoryCache cache)
        {
            _cityService = cityService;
            _categoryAppService = categoryAppService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string cityCacheKey = "citycacheKey";
            string categoryCacheKey = "categoryCacheKey";
            if (!_cache.TryGetValue(cityCacheKey, out List<City> cities))
            {
                cities = await _cityService.GetAll(default);
                _cache.Set(cityCacheKey, cities, TimeSpan.FromDays(90));
            }
            if (!_cache.TryGetValue(categoryCacheKey, out List<Category> categories))
            {
                categories = await _categoryAppService.GetAll(default);
                _cache.Set(categoryCacheKey, categories, TimeSpan.FromHours(24));
            }
            var model = new SearchViewModel
            {
                Cities = cities,
                Categories = categories
            };
            return View(model);
        }
    }
}
