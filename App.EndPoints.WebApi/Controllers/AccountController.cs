using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.AccountDto;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountAppService _accountAppService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountAppService accountAppService,
                                 UserManager<AppUser> userManager)
        {
            _accountAppService = accountAppService;
            _userManager = userManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDto? account)
        {
            if (account is null)
                return BadRequest(new { message = "اطلاعات برای ثبت نام کافی نیست" });

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { message = "اطلاعات وارد شده صحیح نیست", errors });
            }

            if (account.Password != account.ConfirmedPassword)
                return BadRequest(new { message = "رمز عبور یکسان نیست" });

            var existingUser = await _userManager.FindByNameAsync(account.Username);
            if (existingUser != null)
                return Conflict(new { message = "این نام کاربری قبلاً ثبت شده است" });

            var result = await _accountAppService.Register(account);
            if (result.Count == 0)
                return Ok(new { message = "ثبت نام با موفقیت انجام شد", account.Username });
            else
            {
                string message = "";
                foreach (var error in result)
                {
                    message += error + " , ";
                }
                return BadRequest(new { message });
            }
        }
    }
}
