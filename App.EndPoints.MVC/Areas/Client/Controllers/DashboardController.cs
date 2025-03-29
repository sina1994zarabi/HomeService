using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.DTOs.ReviewDto;
using App.Domain.Core.DTOs.ServiceRequestDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.MVC.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Client")]
    public class DashboardController : Controller
    {

        private readonly IClientAppService _clientAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly IServiceRequestAppService _requestAppService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public DashboardController(IClientAppService clientAppService,
            UserManager<AppUser> userManager,
            IServiceAppService serviceAppService,
            IServiceRequestAppService serviceRequestAppService,
            IMemoryCache memoryCache)
        {
            _clientAppService = clientAppService;
            _userManager = userManager;
            _serviceAppService = serviceAppService;
            _requestAppService = serviceRequestAppService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(string username)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = currentUser;
            return View();
        }

        public async Task<IActionResult> ViewProfile()
        {
            //string cachKey = "CurrentUser";
            //if (!_memoryCache.TryGetValue(cachKey, out AppUser model))
            //{
                var model = await _userManager.GetUserAsync(User);
                //_memoryCache.Set(cachKey, model,TimeSpan.FromMinutes(30));
            //}
            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetClientByUserId(user.Id,default);
            var model = new UpdateClientprofileDto
            {
                Id = client.Id,
                AppUserId = client.AppUserId,
                Email = client.AppUser.Email,
                FullName = client.FullName,
                PhoneNumber = client.PhoneNumber,
                ImagePath = client.AppUser.ProfilePicture,
                Username = client.AppUser.UserName
            };
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateClientprofileDto model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _clientAppService.Update(model, default,model.Image);
            return RedirectToAction("ViewProfile");
        }


        public async Task<IActionResult> UpdateBalance()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBalance(decimal balance)
        {
            if (balance <= 100)
            {
                TempData["ErrorMessage"] = "لطفاً مبلغ صحیح وارد کنید.";
                return View(); 
            }
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetClientByUserId(user.Id, default);
            await _clientAppService.UpdateBalance(client.Id, balance, default);
            TempData["SuccessMessage"] = "افزایش موجودی با موفقیت انجام شد.";
            return View();
        }

        public async Task<IActionResult> ViewServices()
        {
            string cachKey = "Services";
            if(!_memoryCache.TryGetValue(cachKey,out List<Service> model))
            {
                model = await _serviceAppService.GetAll(default);
                _memoryCache.Set(cachKey,model,TimeSpan.FromMinutes(30));
            }
            return View(model);
        }

        public async Task<IActionResult> ViewServiceRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            var client = await _clientAppService.GetClientByUserId(user.Id, default);
            //string cachKey = "ServiceRequests";
            //if (!_memoryCache.TryGetValue(cachKey, out List<ServiceRequest> model))
            //{
                  var model = await _clientAppService.GetServiceRequests(client.Id,default);
                //_memoryCache.Set(cachKey, model, TimeSpan.FromMinutes(30));
            //}
            return View(model);
        }

        public async Task<IActionResult> SubmitRequest(int id)
        {
           var service = await _serviceAppService.GetById(id,default);
           ViewBag.Service = service;
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(CreateServiceRequestDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _clientAppService.SubmitServiceRequest(model,default);
            TempData["Message"] = "سفارش با موفقیت ثبت شد";
            return View();
        }
        
        
        public async Task<IActionResult> DeleteServiceRequest(int Id)
        {
            await _requestAppService.Delete(Id, default);
            TempData["SuccessMessage"] = "سفارش با موفقیت حذف شد";
            return RedirectToAction("ViewServiceRequests");
        }

        public async Task<IActionResult> ViewServiceOfferings(int Id)
        {
            ViewBag.ServiceRequest = await _requestAppService.GetById(Id,default);
            var model = await _clientAppService.GetServicesOffering(Id, default);
            return View(model);
        }

        public async Task<IActionResult> LeaveReview(int Id)
        {
            var request = await _requestAppService.GetById(Id,default);
            var createReviewDto = new CreateReviewDto
            {
                ClientId = request.ClientId,
                ServiceOfferingId = request.ServiceOfferings.FirstOrDefault()?.Id ?? 0 
            };

            ViewBag.ServiceRequestId = request.Id;
            ViewBag.ServiceOfferingId = createReviewDto.ServiceOfferingId;
            ViewBag.ClientId = createReviewDto.ClientId;
            

            return View(createReviewDto);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(CreateReviewDto createReviewDto, CancellationToken cancellationToken)
        {
            await _clientAppService.SubmitReview(createReviewDto, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pay(int Id)
        {
            try
            {
                await _clientAppService.ProcessPayment(Id, default);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ViewServiceRequests");
            }
            TempData["SuccessMessage"] = "پرداخت با موفقیت انجام شد";
            return RedirectToAction("ViewServiceRequests");
            
        }
    }
}
