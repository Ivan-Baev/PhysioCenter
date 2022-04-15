namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Reviews;
    using PhysioCenter.Models.Therapists;

    public class ReviewsController : BaseController
    {
        private readonly IClientsService _clientsService;
        private readonly ITherapistsClientsService _therapistsClientsService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _usersManager;
        private readonly IReviewsService _reviewsService;

        public ReviewsController(
            IMapper mapper,
            ITherapistsClientsService therapistsClientsService,
            UserManager<IdentityUser> usersManager,
            IClientsService clientsService,
            IReviewsService reviewsService)
        {
            _mapper = mapper;
            _therapistsClientsService = therapistsClientsService;
            _usersManager = usersManager;
            _clientsService = clientsService;
            _reviewsService = reviewsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _usersManager.GetUserId(User);
            var client = await _clientsService.GetClientByUserId(userId);

            var input = await _therapistsClientsService.GetProvidedClientTherapistsByIdAsync(client.Id);
            var viewModel = _mapper.Map<IEnumerable<TherapistCardViewModel>>(input);

            TempData["ClientId"] = client.Id;

            return View(viewModel);
        }

        public IActionResult WriteReview(string therapistId)
        {
            TempData["TherapistId"] = therapistId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteReview(ReviewInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            var reviewToAdd = _mapper.Map<Review>(input);
            await _reviewsService.AddAsync(reviewToAdd);

            TempData["SuccessfullyAdded"] = Common.Alerts.SuccessfullyAddedReview;

            return this.RedirectToAction(nameof(Index));
        }
    }
}