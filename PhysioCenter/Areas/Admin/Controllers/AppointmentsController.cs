namespace PhysioCenter.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Appointments;

    using System.Threading.Tasks;

    public class AppointmentsController : AdminController
    {
        private readonly IAppointmentsService appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AppointmentsListViewModel
            {
                Appointments =
                    await appointmentsService.GetAllAsync<AppointmentViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            await appointmentsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}