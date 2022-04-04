namespace PhysioCenter.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Adapters;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models;

    using System.Diagnostics;

    public class HomeController : BaseController
    {
        private readonly IBlogsService _blogsService;
        private readonly IReviewsService _reviewsService;
        private readonly IHomeViewModelAdapter _homeViewModelAdapter;

        public HomeController(IBlogsService blogsService,
            IReviewsService reviewsService,
            IHomeViewModelAdapter homeViewModelAdapter)
        {
            _blogsService = blogsService;
            _reviewsService = reviewsService;
            _homeViewModelAdapter = homeViewModelAdapter;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogsService.GetAllAsync();
            var reviews = await _reviewsService.GetAllAsync();

            var viewModel = _homeViewModelAdapter.CreateHomeViewModel(blogs, reviews);
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}