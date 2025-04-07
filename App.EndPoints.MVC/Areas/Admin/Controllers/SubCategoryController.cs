using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.SubCategoryDto;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace App.EndPoints.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ISubCategoryAppService _subCategoryAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(ISubCategoryAppService subCategoryAppService,
            ICategoryAppService categoryAppService,
            ILogger<SubCategoryController> logger)
        {
            _subCategoryAppService = subCategoryAppService;
            _categoryAppService = categoryAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var Subcategories = await _subCategoryAppService.GetAll(default);
            return View(Subcategories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subcategory = await _subCategoryAppService.GetById(id,default);
            if (subcategory == null) { return NotFound(); }
            return View(subcategory);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryAppService.GetAll(default);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubCategoryDto model)
        {
            if (ModelState.IsValid)
                return View(model);
            await _subCategoryAppService.Create(model, default,model.Image);
            var timeStamp = DateTime.Now;
            _logger.LogInformation("[{Timestamp}] اضافه شدن دسته بندی فرعی جدید: {Title}", timeStamp, model.Title);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Categories = await _categoryAppService.GetAll(default);
            ViewBag.CategoryItems = Categories;
            ViewBag.CurrentCategory = Categories.FirstOrDefault(c => c.Subcategories.Any(s => s.Id == id));
            ViewBag.CurrentSubCategory = await _subCategoryAppService.GetById(id,default);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSubCategoryDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _subCategoryAppService.Update(model, default,model.Image);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] ویرایش دسته بندی فرعی : {Title}", timeStamp, model.Title);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _subCategoryAppService.Delete(id, default);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] حذف دسته بندی فرعی : {Id}", timeStamp, id);
            return RedirectToAction("Index");
        }
    }
}
