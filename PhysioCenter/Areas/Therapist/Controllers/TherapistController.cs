namespace PhysioCenter.Areas.Therapist.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Controllers;

    [Authorize(Roles = "Therapist")]
    [Area("Therapist")]
    public class TherapistController : BaseController
    {
    }
}