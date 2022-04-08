namespace PhysioCenter.Areas.Therapist.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Appointments;

    public class AppointmentsController : TherapistController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly ITherapistsService _therapistsService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(IAppointmentsService appointmentsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _appointmentsService = appointmentsService;
            _therapistsService = therapistsService;
            this.mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(DateTime? filterDate)
        {
            var userId = _userManager.GetUserId(User);
            var therapistId = await _therapistsService.FindTherapistById(userId);

            var input = await _appointmentsService.GetTodayByTherapistIdAsync(therapistId.Id, filterDate);
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(input)
            };

            return View(viewModel);
        }
    }
}