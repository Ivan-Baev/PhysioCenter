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

        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var reviewToDelete = await _reviewsService.GetByIdAsync(id);

            var viewModel = _mapper.Map<ReviewViewModel>(reviewToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _reviewsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the category!";

            return RedirectToAction("Index");
        }
    }
}