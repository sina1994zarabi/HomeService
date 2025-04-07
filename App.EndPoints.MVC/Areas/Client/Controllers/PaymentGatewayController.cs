using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class PaymentGatewayController : Controller
    {
        private readonly IServiceRequestAppService _serviceRequestAppService;
        private readonly IServiceOfferingAppService _serviceOfferingAppService;
        private readonly IClientAppService _clientAppService;

        public PaymentGatewayController(IServiceRequestAppService serviceRequestAppService,
                                        IClientAppService clientAppService,
                                        IServiceOfferingAppService serviceOfferingAppService)
        {
            _serviceRequestAppService = serviceRequestAppService;
            _clientAppService = clientAppService;
            _serviceOfferingAppService = serviceOfferingAppService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var model = await _serviceRequestAppService.GetById(Id, default);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int Id)
        {
            try
            {
                await _clientAppService.ProcessPayment(Id,default);
            } 
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            TempData["SuccessMessage"] = "پرداخت با موفقیت انجام شد";
            await _serviceRequestAppService.ChangeStatus(StatusEnum.AwaitingAdminApprovalForTransaction, Id, default);
            var allOffers = await _serviceOfferingAppService.GetAll(default);
            var targetOffer = allOffers.FirstOrDefault(x => ( x.ServiceRequestId == Id && x.Status == StatusEnum.AwaitingAdminApprovalForTransaction));
            await _serviceOfferingAppService.ChangeStatus(targetOffer.Id, StatusEnum.AwaitingAdminApprovalForTransaction, default);
            return RedirectToAction("Index", new {Id = Id});
        }
    }
}
