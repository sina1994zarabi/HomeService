using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                default:
                    return View("InternalServerError");
            }
        }

        [Route("Error/500")]
        public IActionResult HandleException()
        {
            return View("InternalServerError");
        }
    }
}
