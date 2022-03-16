namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Appointments;
    using PhysioCenter.Models.SelectLists;

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
            var clientsSelectList = mapper.Map<IEnumerable<ClientSelectListViewModel>>(clients);

            var therapists = await _therapistsService.GetAllAsync();
            var therapistsSelectList = mapper.Map<IEnumerable<TherapistSelectListViewModel>>(therapists);

            var services = await _servicesService.GetAllAsync();
            var servicesSelectList = mapper.Map<IEnumerable<ServiceSelectListViewModel>>(services);

            ViewData["Clients"] = new SelectList(clientsSelectList, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View();
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

            return RedirectToAction("Index");
        }
    }
}