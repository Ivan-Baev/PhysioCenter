namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Categories;

    public class CategoriesController : AdminController
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public CategoriesController(
            IMapper mapper,
            ICategoriesService categoriesService,
            ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _categoriesService = categoriesService;
            _cloudinaryService = cloudinaryService;
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

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            string imageUrlCloudinary = await _cloudinaryService.UploadFileAsync(input.Image, input.Name);
            var categoryToAdd = _mapper.Map<Category>(input);
            categoryToAdd.ImageUrl = imageUrlCloudinary;
            await _categoriesService.AddAsync(categoryToAdd);

            TempData["SuccessfullyAdded"] = "You have successfully created a new blog post!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditCategory(Guid id)
        {
            var category = await _categoriesService.GetByIdAsync(id);

            var viewModel = _mapper.Map<CategoryEditViewModel>(category);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryEditViewModel input)

        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            if (input.Image != null)
            {
                await _cloudinaryService.DeleteFileAsync(input.ImageUrl);
                input.ImageUrl = await _cloudinaryService.UploadFileAsync(input.Image, input.Name);
            }

            var categoryToEdit = await _categoriesService.GetByIdAsync(input.Id);

            var result = _mapper.Map(input, categoryToEdit);

            await _categoriesService.UpdateDetailsAsync(result);

            TempData["SuccessfullyEdited"] = "You have successfully edited the category!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var categoryToDelete = await _categoriesService.GetByIdAsync(id);

            var viewModel = _mapper.Map<CategoryViewModel>(categoryToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, string imageUrl)
        {
            await _cloudinaryService.DeleteFileAsync(imageUrl);
            await _categoriesService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the category!";

            return RedirectToAction("Index");
        }
    }
}