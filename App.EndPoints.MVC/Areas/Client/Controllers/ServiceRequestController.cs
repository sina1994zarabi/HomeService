using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Entities.User;
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
		private readonly UserManager<AppUser> _userManager;

        public ServiceRequestController(IClientAppService clientAppService,
										IServiceRequestAppService serviceRequestAppService,
										UserManager<AppUser> userManager)
        {
			_clientAppService = clientAppService;
			_serviceRequestAppService = serviceRequestAppService;
			_userManager = userManager;
        }

		// View All ServceRequests
        public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			var client = await _clientAppService.GetById(user.Id,default);
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

		

    }
}
