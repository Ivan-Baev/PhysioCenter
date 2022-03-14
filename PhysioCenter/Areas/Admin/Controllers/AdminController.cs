namespace PhysioCenter.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Controllers;

    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class AdminController : BaseController
    {
    }
}