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
        private readonly IServiceOfferingAppService _serviceOfferingAppService;
        private readonly IExpertAppService _expertAppService;

        public ServiceRequestController(IServiceRequestAppService serviceRequestAppService,
                                        IExpertAppService expertAppService,
                                        IServiceOfferingAppService serviceOfferingAppService)
        {
            _serviceRequestAppService = serviceRequestAppService;
            _expertAppService = expertAppService;
            _serviceOfferingAppService = serviceOfferingAppService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _serviceRequestAppService.GetAll(default);
            return View(model);
        }
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var serviceRequest = await _serviceRequestAppService.GetById(id, default);
            var model = new ChangeStatusDto { 
                          ServiceRequestId = id ,
                          CurrentStatus = serviceRequest.Status
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto model)
        {
            var serviceRequest = await _serviceRequestAppService.GetById(model.ServiceRequestId, default);
            if (serviceRequest == null)
            {
                TempData["Error"] = "سفارش یافت نشد.";
                return RedirectToAction("Index");
            }
            var currentStatus = serviceRequest.Status;
            var newStatus = model.Status;
            if (newStatus == StatusEnum.Paid)
                TempData["Error"] = "تغییر وضعیت به 'پرداخت شده' مجاز نیست.";
            else if (currentStatus == StatusEnum.AwaitingPayment)
                TempData["Error"] = "در وضعیت 'در انتظار پرداخت' نمی‌توان تغییری ایجاد کرد.";
            else if (currentStatus == StatusEnum.Paid)
                TempData["Error"] = "در وضعیت 'پرداخت شده' نمی‌توان تغییری ایجاد کرد.";
            else if (currentStatus == StatusEnum.InProgress && newStatus != StatusEnum.Completed)
                TempData["Error"] = "در وضعیت 'در حال انجام' فقط می‌توان به 'تکمیل شده' تغییر وضعیت داد.";
            else if (currentStatus == StatusEnum.AwaitingOffers && newStatus != StatusEnum.Cancelled)
                TempData["Error"] = "در وضعیت 'در انتظار پیشنهادات' فقط لغو سفارش مجاز است.";
            else if (currentStatus == StatusEnum.PendingClientConfirmation &&
                     newStatus != StatusEnum.PendingProviderConfirmation)
                TempData["Error"] = "در وضعیت 'در انتظار تایید مشتری' فقط می‌توان به 'در انتظار تایید متخصص' تغییر وضعیت داد.";
            else
            {
                await _serviceRequestAppService.ChangeStatus(newStatus, model.ServiceRequestId, default);
                TempData["Success"] = "وضعیت سفارش با موفقیت تغییر کرد.";
                return RedirectToAction("ChangeStatus", new { id = model.ServiceRequestId });
            }
            return RedirectToAction("ChangeStatus", new { id = model.ServiceRequestId });
        }

        public async Task<IActionResult> MarkAsDone(int id,CancellationToken cancellationToken)
        {
            await _serviceRequestAppService.ChangeStatus(StatusEnum.AwaitingOffers,id,cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmTransaction(int requestId)
        {
            var request = await _serviceRequestAppService.GetById(requestId,default);
            var offer = await _serviceRequestAppService.GetApprovedOffer(request.Id,default);
            decimal amount = request.Service.Price;
            decimal fee = amount * 0.1m;
            decimal paymentAmount = amount - fee;
            await _expertAppService.ProccessPayment(offer.Expert.Id,paymentAmount,default);
            await _serviceRequestAppService.ChangeStatus(StatusEnum.Paid,request.Id, default);
            await _serviceOfferingAppService.ChangeStatus(offer.Id,StatusEnum.Paid,default);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _serviceRequestAppService.GetById(id, default);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _serviceRequestAppService.Delete(id,default);
            TempData["Success"] = "سفارش با موفقیت پاک شد";
            return RedirectToAction("Index");
        }
    }
}
