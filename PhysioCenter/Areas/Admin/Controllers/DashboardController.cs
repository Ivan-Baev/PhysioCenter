namespace PhysioCenter.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}