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

        public ClientsController(
            ITherapistsService therapistsService,
            IServicesService servicesService,
            IMapper mapper,
            UserManager<IdentityUser> usersManager,
            IClientsService _clientsService)
        {
            _therapistsService = therapistsService;
            _servicesService = servicesService;
            _mapper = mapper;
            _usersManager = usersManager;
            clientsService = _clientsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _usersManager.GetUserId(User);
            var therapist = await _therapistsService.GetByUserIdAsync(userId);

            var results = await clientsService.GetAllByIdAsync(therapist.Id.ToString());
            ViewBag.TherapistId = therapist.Id.ToString();
            var viewModel = new ClientsListViewModel
            {
                Clients = _mapper.Map<IEnumerable<ClientViewModel>>(results)
            };

            return View(viewModel);
        }
    }
}