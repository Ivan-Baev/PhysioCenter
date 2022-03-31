namespace PhysioCenter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TherapistsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}