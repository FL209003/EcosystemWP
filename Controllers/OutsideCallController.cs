using Microsoft.AspNetCore.Mvc;

namespace EcosystemApp.Controllers
{
    public class OutsideCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
