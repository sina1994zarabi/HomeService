using App.Domain.Core.Contract.AppService;
using App.Domain.Core.DTOs.ExpertDto;
using App.Domain.Core.Entities.Services;
using App.Domain.Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class DashboardController : Controller
    {
        private readonly IExpertAppService _expertAppService;
        private readonly IReviewAppService _reviewAppService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(IExpertAppService expertAppService,
                                   UserManager<AppUser> userManager,
                                   IReviewAppService reviewAppService)
        {
            _expertAppService = expertAppService;
            _userManager = userManager;
            _reviewAppService = reviewAppService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUser = currentUser;
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
            var model = await _expertAppService.GetExpertProfile(expert.Id, default);
            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
            var model = new UpdateExpertDto
            {
                Id = expert.Id,
                AppUserId = expert.AppUserId,
                Email = expert.AppUser.Email,
                FullName = expert.FullName,
                PhoneNumber = expert.PhoneNumber,
                ImagePath = expert.AppUser.ProfilePicture,
                Username = expert.AppUser.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UpdateExpertDto model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await _expertAppService.Update(model, default);
            TempData["Message"] = "ویرایش با موفقیت انجام شد";
            return RedirectToAction("EditProfile");   
        }

        public async Task<IActionResult> ExpertPortfolio()
        {
            var user = await _userManager.GetUserAsync(User);
            var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
            ViewBag.Expert = expert;
            var model = await _expertAppService.GetExpertReview(expert.Id, default);
            return View(model);
        }

        public async Task<IActionResult> Portfolio(int? expertId = null)
        {
            int idToUse;
            if (expertId.HasValue)
                idToUse = expertId.Value;
            else
            {
                var user = await _userManager.GetUserAsync(User);
                var expert = await _expertAppService.GetExpertByUserId(user.Id, default);
                if (expert == null)
                {
                    return NotFound();
                }
                idToUse = expert.Id;
            }
            var expertToDisplay = await _expertAppService.GetById(idToUse, default);

            if (expertToDisplay == null)
            {
                return NotFound(); 
            }

            var reviews = await _reviewAppService.GetByExpertId(expertToDisplay.Id, default);
            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            var Model = new ExpertPortfolioDto
            {
                FullName = expertToDisplay.FullName,
                ProfilePicture = expertToDisplay.AppUser.ProfilePicture,
                Gender = expertToDisplay.Gender,
                PhoneNumber = expertToDisplay.PhoneNumber,
                Skills = expertToDisplay.Services.ToList(),
                AverageRating = averageRating,
                Reviews = reviews
            };
            return View(Model);
        }
    }
}
