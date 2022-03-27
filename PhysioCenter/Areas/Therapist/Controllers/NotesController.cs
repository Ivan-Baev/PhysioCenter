namespace PhysioCenter.Areas.Therapist.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Notes;

    public class NotesController : TherapistController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly INotesService _notesService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public NotesController(IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            ITherapistsServicesService therapistsServicesService,
            UserManager<IdentityUser> userManager,
            INotesService notesService)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            _mapper = mapper;
            _therapistsServicesService = therapistsServicesService;
            _userManager = userManager;
            _notesService = notesService;
        }

        public async Task<IActionResult> Index(string clientId)
        {
            var input = await _notesService.GetAllByClientIdAsync(clientId);

            var viewModel = new NotesListViewModel
            {
                Notes = _mapper.Map<IEnumerable<NoteViewModel>>(input)
            };

            return View(viewModel);
        }
    }
}