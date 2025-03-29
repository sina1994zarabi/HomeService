using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestAppService _serviceRequestAppService;

        public ServiceRequestController(IServiceRequestAppService serviceRequestAppService)
        {
            _serviceRequestAppService = serviceRequestAppService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _serviceRequestAppService.GetAll(default);
            return View(model);
        }

        public IActionResult ChangeStatus(int id)
        {
            return View(new ChangeStatusDto() { ServiceRequestId = id});
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _serviceRequestAppService.ChangeStatus(model.Status,model.ServiceRequestId,default);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarkAsDone(int id,CancellationToken cancellationToken)
        {
            await _serviceRequestAppService.ChangeStatus(StatusEnum.AwaitingOffers,id,cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _serviceRequestAppService.GetById(id, default);
            return View(model);
        }
    }
}
