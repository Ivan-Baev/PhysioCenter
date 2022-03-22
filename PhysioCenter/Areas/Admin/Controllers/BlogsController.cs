namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Blogs;

    public class BlogsController : AdminController
    {
        private readonly IBlogsService _blogsService;
        private readonly IMapper _mapper;

        public BlogsController(
            IMapper mapper,
            IBlogsService blogsService)
        {
            _mapper = mapper;
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
    }
}
