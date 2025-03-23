using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Areas.Expert.Controllers
{
    public class ExpertSkillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
