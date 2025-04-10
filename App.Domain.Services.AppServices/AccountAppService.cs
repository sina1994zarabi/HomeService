using App.Domain.Core.Contract.AppService;
using App.Domain.Core.Contract.Services;
using App.Domain.Core.DTOs.AccountDto;
using App.Domain.Core.DTOs.ClientDto;
using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.AppServices
{
    public class AccountAppService : IAccountAppService
    {
        // Inject Dependencies
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClientService _clientService;
        private readonly IExpertService _expertService;

        public AccountAppService(SignInManager<AppUser> signInManager,
                                 UserManager<AppUser> userManager,
                                 IClientService clientService,
                                 IExpertService expertService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientService = clientService;
            _expertService = expertService;
        }

        public async Task<IdentityResult> DeleteUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            return await _userManager.DeleteAsync(user);
        }


		public async Task<string> GetRedirectUrlForUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return "/";

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return "~/Admin/Dashboard/Index";
            else if (await _userManager.IsInRoleAsync(user, "Expert"))
                return "~/Expert/Dashboard/Index";
            else if (await _userManager.IsInRoleAsync(user, "Client"))
                return "~/Client/Dashboard/Index";
            else return "/";
        }

        public async Task<bool> Login(AccountLoginDto accountLoginDto)
        {
            var result = await _signInManager.
                PasswordSignInAsync(accountLoginDto.Username,
                                    accountLoginDto.Password,
                                    isPersistent:false,
                                    lockoutOnFailure:false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<IdentityError>> Register(AccountRegisterDto accountRegisterDto)
        {
            var user = CreateUser();
            user.Email = accountRegisterDto.Email;
            user.UserName = accountRegisterDto.Username;
            user.FullName = accountRegisterDto.FullName;     
            user.Gender = accountRegisterDto.Gender;           
            user.PhoneNumber = accountRegisterDto.PhoneNumber; 

            
            var result = await _userManager.CreateAsync(user, accountRegisterDto.Password);
            if (!result.Succeeded)
                return result.Errors.ToList();
            
            if (accountRegisterDto.Role == "Client")
            {
                var client = new CreateClientDto
                {
                    AppUserId = user.Id,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber
                };
                await _clientService.Add(client,default);
                await _userManager.AddToRoleAsync(user, "Client");
            }
            else if (accountRegisterDto.Role == "Expert")
            {
                var expert = new CreateExpertDto
                {
                    AppUserId = user.Id,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber
                };
                await _expertService.Add(expert, default);
                await _userManager.AddToRoleAsync(user, "Expert");
            }
            return result.Errors.ToList();
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateAccountDto updateDto)
        {
            var user = await _userManager.FindByIdAsync(updateDto.Id.ToString());
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "کاربر یافت نشد" });
            user.FullName = updateDto.FullName;
            user.UserName = updateDto.Username;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.Email = updateDto.Email;
            return await _userManager.UpdateAsync(user);
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Cant Create An Instance Of {nameof(AppUser)}");
            }
        }
    }
}
