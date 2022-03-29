namespace PhysioCenter.Areas.Therapist.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Newtonsoft.Json;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Appointments;

    public class AppointmentsController : TherapistController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            ITherapistsServicesService therapistsServicesService,
            UserManager<IdentityUser> userManager)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            this.mapper = mapper;
            _therapistsServicesService = therapistsServicesService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var therapist = await _therapistsService.GetByUserIdAsync(userId);

            var input = await _appointmentsService.GetAllByTherapistIdAsync(therapist.Id.ToString());
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(input)
            };

            return View(viewModel);
        }
    }
}