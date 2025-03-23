using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class SkillController : Controller
    {
        private readonly IExpertAppService _expertAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly UserManager<AppUser> _userManager;

        public SkillController(IExpertAppService expertAppService,
                                     IServiceAppService serviceAppServce,
                                     UserManager<AppUser> userManager)
        {
            _serviceAppService = serviceAppServce;
            _expertAppService = expertAppService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = await _expertAppService.GetExpertSkills(user.Id, default);
            var message = TempData["Message"] as string;
            ViewBag.Message = message;
            return View(model);
        }


        public async Task<IActionResult> AddServiceToSkills(int Id)
        {
            var service = await _serviceAppService.GetById(Id, default);
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
            await _expertAppService.UpdateSkills(expert.Id, service, default);
            TempData["Message"] = "سرویس با موفقیت به مهارت ها اضافه شد";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveServiceFromSkills(int id)
        {
            var service = await _serviceAppService.GetById(id, default);
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
            await _expertAppService.RemoveSkills(expert.Id, service, default);
            TempData["Message"] = "سرویس از لیست مهارت ها پاک شد";
            return RedirectToAction("Index");
        }
    }
}
