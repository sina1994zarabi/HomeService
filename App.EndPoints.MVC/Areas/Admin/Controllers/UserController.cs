using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.AccountDto;
using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.Entities.User;
using App.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace App.EndPoints.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        #region Dependencies
        private readonly IClientAppService _clientAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<UserController> _logger;
        

        public UserController(IClientAppService clientAppService,
                              IExpertAppService expertAppService,
                              IAccountAppService accountAppService,
                              ILogger<UserController> logger
                              )
        {
            _clientAppService = clientAppService;
            _expertAppService = expertAppService;
            _accountAppService = accountAppService;
            _logger = logger;
        }
        #endregion 


        public IActionResult Index() { return View(); }


        #region Clients
        public async Task<IActionResult> Clients()
        {
            var clients = await _clientAppService.GetAll(default);
            return View(clients);
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(AccountRegisterDto model)
        {
            model.Role = "Client";
            if (!ModelState.IsValid)
                return View(model);
            var result = await _accountAppService.Register(model);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] مشتری جدید اضافه شد با نام کاربری: {Username}", timeStamp, model.Username);
            if (result.Count == 0)
                return RedirectToAction("Clients");
            foreach (var error in result)
                ModelState.AddModelError("", error.Description);
            return View(model);
        } 


        public async Task<IActionResult> ClientDetails(int id)
        {
            var client = await _clientAppService.GetClientInfo(id, default);
            return View(client);
        }

        public async Task<IActionResult> EditClient(int id)
        {
            var client = await _clientAppService.GetClientInfo(id, default);
            var model = new UpdateClientprofileDto
            {
                Id = id,
                AppUserId = client.AppUserId,
                FullName = client.FullName,
                Email = client.AppUser.Email,
                PhoneNumber = client.PhoneNumber,
                Username = client.AppUser.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditClient(UpdateClientprofileDto model)
        {
            await _clientAppService.Update(model, default, model.Image);
            var updateAccountDto = new UpdateAccountDto
            {
                Id = model.AppUserId,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username,
            };
            await _accountAppService.UpdateUserAsync(updateAccountDto);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] مشخصات مشتری با نام کاربری ویرایش شد: {Username}", timeStamp, model.Username);
            return RedirectToAction("Clients");
        }
        
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientToDelete = await _clientAppService.GetById(id, default);
            await _accountAppService.DeleteUserAsync(clientToDelete.AppUserId);
            await _clientAppService.Delete(id, default);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] مشتری حذف شد: {Id}", timeStamp, id);
            return RedirectToAction("Clients");
        }



        #endregion

        #region Experts
        public async Task<IActionResult> Experts()
        {
            var experts = await _expertAppService.GetAll(default);
            return View(experts);
        }

        public IActionResult CreateExpert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpert(AccountRegisterDto model)
        {
            model.Role = "Expert";
            if (!ModelState.IsValid)
                return View(model);
            var result = await _accountAppService.Register(model);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] کارشناس جدید اضافه شد با نام کاربری: {Username}", timeStamp, model.Username);
            if (result.Count == 0)
                return RedirectToAction("Experts");
            foreach (var error in result)
                ModelState.AddModelError("", error.Description);
            return View(model);
        }

        public async Task<IActionResult> ExpertDetails(int id)
        {
            var expert = await _expertAppService.GetExpertInfo(id, default);
            return View(expert);
        }

        public async Task<IActionResult> EditExpert(int id)
        {
            var expert = await _expertAppService.GetExpertInfo(id,default);
            var model = new UpdateExpertDto
            {
                Id = id,
                AppUserId = expert.AppUserId,
                FullName = expert.FullName,
                Email = expert.AppUser.Email,
                PhoneNumber = expert.PhoneNumber,
                Username = expert.AppUser.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditExpert(UpdateExpertDto model)
        {
            await _expertAppService.Update(model, default);
            var updateAccountDto = new UpdateAccountDto
            {
                Id = model.AppUserId,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username
            };
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] مشخصات کارشناس با نام کاربری ویرایش شد: {Username}", timeStamp, model.Username);
            await _accountAppService.UpdateUserAsync(updateAccountDto);
            return RedirectToAction("Experts");
        }

        
        public async Task<IActionResult> Approve(int id)
        {
            await _expertAppService.ChangeStatus(id, default);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] کارشناس با نام کاربری تایید شد: {Username}", timeStamp, id);
            return RedirectToAction("Experts");
        }

        public async Task<IActionResult> DeleteExpert(int id)
        {
            var expertToDelete = await _expertAppService.GetById(id, default);
            await _accountAppService.DeleteUserAsync(expertToDelete.AppUserId);
            await _expertAppService.Delete(id, default);
            var timeStamp = DateTime.Now;
            _logger.LogWarning("[{Timestamp}] کارشناس حذف شد: {Id}", timeStamp, id);
            return RedirectToAction("Experts");
        }

        #endregion 
    }
}
