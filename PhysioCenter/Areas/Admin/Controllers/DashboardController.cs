namespace PhysioCenter.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdminController
    {
        //maybe add more navigation or something or delete if not used!

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LeaveAdminArea()
        {
            return Redirect("/");
        }
    }
}