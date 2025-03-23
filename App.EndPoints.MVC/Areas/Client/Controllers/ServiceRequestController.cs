using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Repository;
using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.User;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;

namespace App.EndPoints.MVC.Areas.Client.Controllers
{
	[Area("Client")]
	public class ServiceRequestController : Controller
	{
		private readonly IClientAppService _clientAppService;
		private readonly IServiceRequestAppService _serviceRequestAppService;
		private readonly IServiceOfferingAppService _serviceOfferingAppService;
		private readonly IServiceAppService _serviceAppService;
        private readonly IReviewAppService _reviewAppService;
		private readonly UserManager<AppUser> _userManager;

        public ServiceRequestController(IClientAppService clientAppService,
										IServiceRequestAppService serviceRequestAppService,
                                        UserManager<AppUser> userManager,
                                        IServiceAppService serviceAppService,
                                        IReviewAppService reviewAppService,
                                        IServiceOfferingAppService serviceOfferingAppService)
        {
            _clientAppService = clientAppService;
            _serviceRequestAppService = serviceRequestAppService;
            _userManager = userManager;
            _serviceAppService = serviceAppService;
            _reviewAppService = reviewAppService;
            _serviceOfferingAppService = serviceOfferingAppService;
        }

        // View All ServceRequests
        public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			var client = await _clientAppService.GetClientByUserId(user.Id,default);
			var model = await _clientAppService.GetServiceRequests(client.Id,default);
			return View(model);
		}

        // View Completed ServiceRequests
        public async Task<IActionResult> ViewCompletedServiceRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetById(user.Id, default);
            var model = await _clientAppService.GetServiceRequests(client.Id, default);
            return View(model);
        }

		public async Task<IActionResult> SubmitRequest(int Id)
		{
			var service = await _serviceAppService.GetById(Id, default);
			ViewBag.Service = service;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SubmitRequest(CreateServiceRequestDto model)
		{
			if (!ModelState.IsValid)
				return View(model);
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetClientByUserId(user.Id, default);
			model.ClientId = client.Id;
            await _clientAppService.SubmitServiceRequest(model, default);
			TempData["Message"] = "سفارش با موفقیت ثبت شد";
			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Details(int Id)
        {
			var model = await _serviceRequestAppService.GetById(Id, default);
			return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptOffer(int requestId, int offerId)
        {
            await _serviceRequestAppService.AcceptOffer(requestId, offerId,default);
            TempData["Message"] = "پیشنهاد با موفقیت انتخاب شد";
            return RedirectToAction("Details", new { id = requestId });
        }

        public async Task<IActionResult> SubmitReview(int Id)
        {
            var servieOfferings = await _serviceOfferingAppService.GetAll(default);
            var offering = servieOfferings.FirstOrDefault(x => x.Id == Id);
            ViewBag.Offering = offering;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(CreateReviewDto model)
        {
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetClientByUserId(user.Id, default);
            model.ClientId = client.Id;
            if (!ModelState.IsValid)
                return View(model);
            await _reviewAppService.Create(model, default);
            TempData["Message"] = "نظر با موفقیت ثبت شد";
            var offers = await _serviceOfferingAppService.GetAll(default);
            var offer = offers.FirstOrDefault(x => x.Id == model.ServiceOfferingId);
            ViewBag.Offering = offer;
            return View();
        }
    }
}
