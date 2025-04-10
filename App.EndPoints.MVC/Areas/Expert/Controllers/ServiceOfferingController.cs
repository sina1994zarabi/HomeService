using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.ServiceOfferingDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using App.Domain.Core.Enums;
using App.Domain.Services.AppServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class ServiceOfferingController : Controller
    {
        private readonly IServiceOfferingAppService _serviceOfferingAppService;
        private readonly IServiceRequestAppService _serviceRequestAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly UserManager<AppUser> _userManager;

        public ServiceOfferingController(IServiceOfferingAppService serviceOfferingAppService,
                                         IExpertAppService expertAppService,
                                         UserManager<AppUser> userManager,
                                         IServiceRequestAppService serviceRequestAppService)
        {
            _serviceOfferingAppService = serviceOfferingAppService;
            _expertAppService = expertAppService;
            _userManager = userManager;
            _serviceRequestAppService = serviceRequestAppService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = await _expertAppService.GetServiceOfferings(user.Id,default);
            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await _serviceOfferingAppService.GetById(Id, default);
            return View(model);
        }

        public async Task<IActionResult> Confirm(int OfferId,int RequestId)
        {
            await _serviceOfferingAppService.ChangeStatus(OfferId, StatusEnum.InProgress, default);
            await _serviceRequestAppService.ChangeStatus(StatusEnum.InProgress, RequestId, default);
            TempData["Message"] = "با موفقیت تایید شد";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SubmitOffer(int Id)
        {
            var serviceRequest = await _serviceRequestAppService.GetById(Id,default);
            ViewBag.ServiceRequest = serviceRequest;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitOffer(CreateServiceOfferingDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id,default);
            model.ExpertId = expert.Id;
            var offerId = await _serviceOfferingAppService.Create(model, default);
            await _serviceRequestAppService.ChangeStatus(StatusEnum.PendingClientConfirmation,model.ServiceRequestId,default);
            await _serviceOfferingAppService.ChangeStatus(offerId, StatusEnum.PendingClientConfirmation,default);
            TempData["Message"] = "پیشنهاد شما با موفقیت ارسال شد.";
            return RedirectToAction("SubmitOffer", "ServiceOffering", new { id = model.ServiceRequestId }); 
        }
    }
}
