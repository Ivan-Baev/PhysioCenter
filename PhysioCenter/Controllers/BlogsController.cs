namespace PhysioCenter.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Blogs;

    public class BlogsController : BaseController
    {
        private readonly IServicesService _servicesService;
        private readonly IBlogsService _blogsService;
        private readonly IMapper _mapper;

        public BlogsController(
            IMapper mapper,
            IServicesService servicesService,
            IBlogsService blogsService)
        {
            _mapper = mapper;
            _servicesService = servicesService;
            _blogsService = blogsService;
        }

        public async Task<IActionResult> Index()
        {
            var input = await _blogsService.GetAllAsync();
            var viewModel = new BlogsListViewModel
            {
                Blogs = _mapper.Map<IEnumerable<BlogViewModel>>(input)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var input = await _blogsService.GetAllAsync();
            var viewModel = new BlogsListViewModel
            {
                Blogs = _mapper.Map<IEnumerable<BlogViewModel>>(input)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(string blogId)
        {
            var input = await _blogsService.GetByIdAsync(blogId);
            var viewModel = _mapper.Map<BlogViewModel>(input);
            return View(viewModel);
        }
    }
}