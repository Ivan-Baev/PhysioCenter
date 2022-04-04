namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Newtonsoft.Json;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;

    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentsService appointmentsService;
        private readonly IServicesService servicesService;
        private readonly IClientsService clientsService;
        private readonly ITherapistsService therapistsService;
        private readonly ITherapistsClientsService therapistsClientsService;
        private readonly UserManager<IdentityUser> _usersManager;
        private readonly IMapper mapper;

        public AppointmentsController(
            IAppointmentsService _appointmentsService,
            IServicesService _servicesService,
            IClientsService _clientsService,
            ITherapistsService _therapistsService,
            IMapper _mapper,
            ITherapistsClientsService _therapistsClientsService,
            UserManager<IdentityUser> usersManager)
        {
            appointmentsService = _appointmentsService;
            servicesService = _servicesService;
            clientsService = _clientsService;
            therapistsService = _therapistsService;
            mapper = _mapper;
            therapistsClientsService = _therapistsClientsService;
            _usersManager = usersManager;
        }

        [Authorize(Roles = "Standard User")]
        public async Task<IActionResult> BookAppointment()
        {
            var userId = _usersManager.GetUserId(User);
            var client = await clientsService.GetClientByUserId(userId);
            var therapists = await therapistsService.GetAllAsync();
            var services = await servicesService.GetAllAsync();

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

            var appointment = mapper.Map<Appointment>(input);
            await appointmentsService.AddAsync(appointment);

            var therapistClient = mapper.Map<TherapistClient>(appointment);
            await therapistsClientsService.AddTherapistClientAsync(therapistClient);

            TempData["SuccessfullyAdded"] = "You have successfully booked an appointment!";

            return this.RedirectToAction("Index", "Home");
        }
    }
}