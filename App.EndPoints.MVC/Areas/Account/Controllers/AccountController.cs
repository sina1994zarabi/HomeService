using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.AccountDto;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Account.Controllers
{
    [Area("Account")]
    
    public class AccountController : Controller
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IClientAppService _clientAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountAppService accountAppService,
                                 IClientAppService clientAppService,
                                 IExpertAppService expertAppService,
                                 ILogger<AccountController> logger
                                 )
        {
            _accountAppService = accountAppService;
            _clientAppService = clientAppService;
            _expertAppService = expertAppService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginDto model)
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            if (!ModelState.IsValid)
            {
				return View(model);
            }
            var result = await _accountAppService.Login(model);
			if (result)
            {
                _logger.LogInformation("[{Timestamp}] ورود موفق کاربر: {Username}."
                    , timestamp,
                    model.Username);
                
				string redirectUrl = await _accountAppService.GetRedirectUrlForUser(model.Username);
                return Redirect(redirectUrl);
            }
			_logger.LogWarning("[{Timestamp}] ورود ناموفق کاربر: {Username}", timestamp, model.Username);
			ModelState.AddModelError("", "ورود ناموفق");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new AccountRegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _accountAppService.Register(model);
            if (result.Count == 0)
                return RedirectToAction("Login");
            foreach (var error in result)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            var timestamp = DateTime.Now;
			_logger.LogInformation("[{Timestamp}] خروج موفق کاربر: {Username}", timestamp, User.Identity.Name);
			await _accountAppService.Logout();
            return RedirectToAction("Index", "Home",new { area = ""});
        }
    }
}
