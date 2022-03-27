namespace PhysioCenter.Areas.Therapist.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : TherapistController
    {
        //maybe add more navigation or something or delete if not used!

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LeaveTherapistArea()
        {
            return Redirect("/");
        }
    }
}