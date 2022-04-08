namespace PhysioCenter.Areas.Therapist.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Clients;

    public class ClientsController : TherapistController
    {
        private readonly ITherapistsService _therapistsService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService clientsService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _usersManager;
        private readonly ITherapistsClientsService _therapistsClientsService;

        public ClientsController(
            ITherapistsService therapistsService,
            IServicesService servicesService,
            IMapper mapper,
            UserManager<IdentityUser> usersManager,
            IClientsService _clientsService,
            ITherapistsClientsService therapistsClientsService)
        {
            _therapistsService = therapistsService;
            _servicesService = servicesService;
            _mapper = mapper;
            _usersManager = usersManager;
            clientsService = _clientsService;
            _therapistsClientsService = therapistsClientsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _usersManager.GetUserId(User);
            var therapistId = _therapistsService.FindTherapistId(userId);
            var clients = await _therapistsClientsService.GetProvidedTherapistClientsByIdAsync(therapistId);
            ViewBag.TherapistId = therapistId;
            var viewModel = new ClientsListViewModel
            {
                Clients = _mapper.Map<IEnumerable<ClientViewModel>>(clients)
            };

            return View(viewModel);
        }
    }
}