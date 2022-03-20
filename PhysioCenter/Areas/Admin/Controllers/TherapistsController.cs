namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models.Therapists;

    public class TherapistsController : AdminController
    {
        private readonly ITherapistsService _therapistsService;
        private readonly IMapper _mapper;

        public TherapistsController(
            ITherapistsService therapistsService,
            IMapper mapper)
        {
            _therapistsService = therapistsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _therapistsService.GetAllAsync();
            var viewModel = new TherapistsListViewModel
            {
                Therapists = _mapper.Map<IEnumerable<TherapistViewModel>>(results)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var therapistToDelete = await _therapistsService.GetByIdAsync(id);

            var viewModel = _mapper.Map<TherapistViewModel>(therapistToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _therapistsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the therapist!";

            return RedirectToAction("Index");
        }
    }
}
