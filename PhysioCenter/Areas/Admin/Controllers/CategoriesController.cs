namespace PhysioCenter.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;

    public class CategoriesController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
