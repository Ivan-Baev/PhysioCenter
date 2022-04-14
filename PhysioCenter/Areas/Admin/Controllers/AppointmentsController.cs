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

        public async Task<IActionResult> Index(string? clientName, int page = 1, int pageSize = 10)
        {
            var input = await appointmentsService.GetAllAsync(page, pageSize, clientName);

            ViewData["CurrentFilter"] = clientName;

            var viewModel = new AppointmentsListViewModel
            {
                ItemCount = await appointmentsService.GetCount(clientName),
                ItemsPerPage = pageSize,
                CurrentPage = page,
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(input)
            };

            if (page > viewModel.PagesCount || page <= 0)
            {
                return this.NotFound();
            }

            return viewModel == null ? this.NotFound() : this.View(viewModel);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            var clients = await clientsService.GetAllAsync();
            var therapists = await therapistsService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                var clients = await clientsService.GetAllAsync();
                var therapists = await therapistsService.GetAllAsync();
                var services = await therapistsServicesService.GetProvidedTherapistServicesByIdAsync(input.TherapistId);

                ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
                ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
                ViewData["Services"] = new SelectList(services, "ServiceId", "Service.Name");

                return View();
            }

            var appointment = mapper.Map<Appointment>(input);
            await appointmentsService.AddAsync(appointment);

            var therapistClient = mapper.Map<TherapistClient>(appointment);
            await therapistsClientsService.AddTherapistClientAsync(therapistClient);

            TempData["SuccessfullyAdded"] = "You have successfully created a new appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAppointment(Guid id)
        {
            var appointmentToEdit = await appointmentsService.GetByIdAsync(id);
            var viewModel = mapper.Map<AppointmentEditViewModel>(appointmentToEdit);

            var clients = await clientsService.GetAllAsync();
            var therapists = await therapistsService.GetAllAsync();
            var services = await therapistsServicesService.GetProvidedTherapistServicesByIdAsync(appointmentToEdit.TherapistId);

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["ServicesTest"] = new SelectList(services, "ServiceId", "Service.Name");

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(AppointmentEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                var clients = await clientsService.GetAllAsync();
                var therapists = await therapistsService.GetAllAsync();
                var services = await therapistsServicesService.GetProvidedTherapistServicesByIdAsync(input.TherapistId);

                ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
                ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
                ViewData["Services"] = new SelectList(services, "ServiceId", "Service.Name");
                return View();
            }

            var appointment = mapper.Map<Appointment>(input);
            await appointmentsService.UpdateAsync(appointment);

            TempData["SuccessfullyEdited"] = "You have successfully edited the appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var appointmentToDelete = await appointmentsService.GetByIdAsync(id);

            var viewModel = mapper.Map<AppointmentViewModel>(appointmentToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await appointmentsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the appointment!";

            return RedirectToAction("Index");
        }
    }
}