namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Categories;

    public class CategoriesController : AdminController
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(
            IMapper mapper,
            ICategoriesService categoriesService)
        {
            _mapper = mapper;
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
    }
}
