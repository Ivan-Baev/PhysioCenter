namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Therapists;

    public class TherapistsController : BaseController
    {
        private readonly ITherapistsService _therapistsService;
        private readonly IMapper _mapper;

        public TherapistsController(
            IMapper mapper,
            ITherapistsService therapistsService)
        {
            _mapper = mapper;
            _therapistsService = therapistsService;
        }

        public async Task<IActionResult> Index()
        {
            var input = await _therapistsService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<TherapistCardViewModel>>(input);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string therapistId)
        {
            var input = await _therapistsService.GetByIdAsync(therapistId);
            var viewModel = _mapper.Map<TherapistCardViewModel>(input);
            return View(viewModel);
        }
    }
}