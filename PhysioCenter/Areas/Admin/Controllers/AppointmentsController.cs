namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Newtonsoft.Json;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;

    using System.Threading.Tasks;

    public class AppointmentsController : AdminController
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IServicesService servicesService;
        private readonly IClientsService clientsService;
        private readonly ITherapistsService therapistsService;
        private readonly ITherapistsServicesService therapistsServicesService;
        private readonly ITherapistsClientsService therapistsClientsService;
        private readonly IMapper mapper;

        public AppointmentsController(
            IAppointmentsService _appointmentsService,
            IServicesService _servicesService,
            IClientsService _clientsService,
            ITherapistsService _therapistsService,
            IMapper _mapper,
            ITherapistsServicesService _therapistsServicesService,
            ITherapistsClientsService _therapistsClientsService)
        {
            appointmentsService = _appointmentsService;
            servicesService = _servicesService;
            clientsService = _clientsService;
            therapistsService = _therapistsService;
            mapper = _mapper;
            therapistsServicesService = _therapistsServicesService;
            therapistsClientsService = _therapistsClientsService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 10;
            var input = await appointmentsService.GetAllAsync();
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(input).Skip((page - 1) * pageSize).Take(pageSize)
            };
            var count = input.Count();
            viewModel.CurrentPage = page;
            viewModel.PageCount = (int)Math.Ceiling((double)count / pageSize) != 0
                ? (int)Math.Ceiling((double)count / pageSize) : 1;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            var clients = await clientsService.GetAllAsync();
            var therapists = await therapistsService.GetAllAsync();
            var services = await servicesService.GetAllAsync();

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
            await appointmentsService.AddAsync(appointment);

            var therapistClient = mapper.Map<TherapistClient>(appointment);
            await therapistsClientsService.AddTherapistClientAsync(therapistClient);

            TempData["SuccessfullyAdded"] = "You have successfully created a new appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAppointment(string id)
        {
            var appointmentToEdit = await appointmentsService.GetByIdAsync(id);
            var viewModel = mapper.Map<AppointmentEditViewModel>(appointmentToEdit);

            var clients = await clientsService.GetAllAsync();
            var therapists = await therapistsService.GetAllAsync();
            var services = await servicesService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(AppointmentEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = mapper.Map<Appointment>(input);
            await appointmentsService.UpdateAsync(appointment);

            TempData["SuccessfullyEdited"] = "You have successfully edited the appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var appointmentToDelete = await appointmentsService.GetByIdAsync(id);

            var viewModel = mapper.Map<AppointmentViewModel>(appointmentToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await appointmentsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the appointment!";

            return RedirectToAction("Index");
        }
    }
}