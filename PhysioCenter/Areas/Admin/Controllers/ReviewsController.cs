namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Reviews;

    public class ReviewsController : AdminController
    {
        private readonly IReviewsService _reviewsService;
        private readonly IMapper _mapper;

        public ReviewsController(
            IMapper mapper,
            IReviewsService reviewsService)
        {
            _mapper = mapper;
            _reviewsService = reviewsService;
        }
        public async Task<IActionResult> Index()
        {
            var input = await _reviewsService.GetAllAsync();
            var viewModel = new ReviewsListViewModel
            {
                Reviews = _mapper.Map<IEnumerable<ReviewViewModel>>(input)
            };
            return View(viewModel);
        }
    }
}
