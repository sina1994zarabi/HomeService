using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Entities.User;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestAppService _serviceRequestAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly UserManager<AppUser> _userManager;


        public ServiceRequestController(IServiceRequestAppService serviceRequestAppService,
                                        IExpertAppService expertAppService,
                                        UserManager<AppUser> userManager)
        {
            _expertAppService = expertAppService;
            _serviceRequestAppService = serviceRequestAppService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = await _expertAppService.GetServiceRequests(user.Id, default);
            return View(model.Where(x => x.Status == StatusEnum.AwaitingOffers ||
                                    x.Status == StatusEnum.PendingClientConfirmation)
                   .ToList());
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await _serviceRequestAppService.GetById(Id, default);
            return View(model);
        }
    }
}
