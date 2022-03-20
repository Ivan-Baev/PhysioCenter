namespace PhysioCenter.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
