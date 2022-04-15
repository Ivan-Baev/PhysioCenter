namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;

    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsClientsService _therapistsClientsService;
        private readonly UserManager<IdentityUser> _usersManager;
        private readonly IMapper _mapper;

        public AppointmentsController(
            IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            ITherapistsClientsService therapistsClientsService,
            UserManager<IdentityUser> usersManager)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            _mapper = mapper;
            _therapistsClientsService = therapistsClientsService;
            _usersManager = usersManager;
        }

        [Authorize(Roles = "Standard User")]
        public async Task<IActionResult> BookAppointment()
        {
            var userId = _usersManager.GetUserId(User);
            var client = await _clientsService.GetClientByUserId(userId);
            var therapists = await _therapistsService.GetAllAsync();
            var services = await _servicesService.GetAllAsync();

            ViewData["ClientId"] = client.Id;
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(AppointmentInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = _mapper.Map<Appointment>(input);
            await _appointmentsService.AddAsync(appointment);

            var therapistClient = _mapper.Map<TherapistClient>(appointment);
            await _therapistsClientsService.AddTherapistClientAsync(therapistClient);

            TempData["SuccessfullyAdded"] = Common.Alerts.SuccessfullyAddedAppointment;

            return this.RedirectToAction("Index", "Home");
        }
    }
}