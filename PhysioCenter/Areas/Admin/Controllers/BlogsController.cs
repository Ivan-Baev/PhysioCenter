namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Blogs;

    public class BlogsController : AdminController
    {
        private readonly IBlogsService _blogsService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public BlogsController(
            IMapper mapper,
            IBlogsService blogsService,
            ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _blogsService = blogsService;
            _cloudinaryService = cloudinaryService;
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

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            string imageUrlCloudinary = await _cloudinaryService.UploadFileAsync(input.Image, input.Title);
            var blogToAdd = _mapper.Map<Blog>(input);
            blogToAdd.ImageUrl = imageUrlCloudinary;
            await _blogsService.AddAsync(blogToAdd);

            TempData["SuccessfullyAdded"] = "You have successfully created a new blog post!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditBlog(Guid id)
        {
            var blogToEdit = await _blogsService.GetByIdAsync(id);

            var viewModel = _mapper.Map<BlogEditViewModel>(blogToEdit);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(BlogEditViewModel input, Guid id)

        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            if (input.Image != null)
            {
                await _cloudinaryService.DeleteFileAsync(input.ImageUrl);
                input.ImageUrl = await _cloudinaryService.UploadFileAsync(input.Image, input.Title);
            }

            var blogToEdit = await _blogsService.GetByIdAsync(id);

            var result = _mapper.Map(input, blogToEdit);

            await _blogsService.UpdateDetailsAsync(result);

            TempData["SuccessfullyEdited"] = "You have successfully edited the blog!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var blogToDelete = await _blogsService.GetByIdAsync(id);

            var viewModel = _mapper.Map<BlogViewModel>(blogToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, string imageUrl)
        {
            await _cloudinaryService.DeleteFileAsync(imageUrl);
            await _blogsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the blog!";

            return RedirectToAction("Index");
        }
    }
}