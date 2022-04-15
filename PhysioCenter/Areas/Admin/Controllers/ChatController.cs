namespace PhysioCenter.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;

    public class ChatController : AdminController
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}