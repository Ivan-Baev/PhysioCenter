namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Categories;
    using PhysioCenter.Models.Services;

    public class ServicesController : BaseController
    {
        private readonly IServicesService _servicesService;
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public ServicesController(
            IMapper mapper,
            IServicesService servicesService,
            ICategoriesService categoriesService)
        {
            _mapper = mapper;
            _servicesService = servicesService;
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var input = await _categoriesService.GetAllAsync();
            var viewModel = new CategoriesListViewModel
            {
                Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(input)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(string serviceId)
        {
            var input = await _servicesService.GetByIdAsync(serviceId);
            var viewModel = _mapper.Map<ServiceViewModel>(input);
            return View(viewModel);
        }
    }
}