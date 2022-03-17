namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models;
    using PhysioCenter.Models.Appointments;

    using System.Threading.Tasks;

    public class AppointmentsController : AdminController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly IMapper mapper;

        public AppointmentsController(IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _appointmentsService.GetAllAsync();
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(results)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            var clients = await _clientsService.GetAllAsync();
            var therapists = await _therapistsService.GetAllAsync();
            var services = await _servicesService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentInputViewModel input)
        {

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = mapper.Map<Appointment>(input);
            await _appointmentsService.AddAsync(appointment);

            TempData["SuccessfullyAdded"] = "You have successfully created a new appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAppointment(string id)
        {
            var appointmentToEdit = await _appointmentsService.GetByIdAsync(id);

            var viewModel = mapper.Map<AppointmentInputViewModel>(appointmentToEdit);


            var clients = await _clientsService.GetAllAsync();
            var therapists = await _therapistsService.GetAllAsync();
            var services = await _servicesService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(AppointmentInputViewModel input, string id)
        {

            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = mapper.Map<Appointment>(input);
            appointment.Id = Guid.Parse(id);
            await _appointmentsService.UpdateAsync(appointment);

            TempData["SuccessfullyEdited"] = "You have successfully edited the appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var appointmentToDelete = await _appointmentsService.GetByIdAsync(id);

            var viewModel = mapper.Map<AppointmentViewModel>(appointmentToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _appointmentsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the appointment!";

            return RedirectToAction("Index");
        }
    }
}