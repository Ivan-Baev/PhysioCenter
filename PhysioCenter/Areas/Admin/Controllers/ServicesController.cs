namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Services;

    public class ServicesController : AdminController
    {
        private readonly IServicesService _servicesService;
        private readonly IMapper _mapper;

        public ServicesController(
            IMapper mapper,
            IServicesService servicesService)
        {
            _mapper = mapper;
            _servicesService = servicesService;
        }
        public async Task<IActionResult> Index()
        {
            var input = await _servicesService.GetAllAsync();
            var viewModel = new ServicesListViewModel
            {
                Services = _mapper.Map<IEnumerable<ServiceViewModel>>(input)
            };
            return View(viewModel);
        }
    }
}
