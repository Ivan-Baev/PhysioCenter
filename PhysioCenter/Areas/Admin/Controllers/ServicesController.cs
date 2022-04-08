namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Services;
    using PhysioCenter.Models.Therapists;

    public class ServicesController : AdminController
    {
        private readonly IServicesService _servicesService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ICategoriesService _categoriesService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsServicesService _therapistsServicesService;

        public ServicesController(
            IMapper mapper,
            IServicesService servicesService,
            ICloudinaryService cloudinaryService,
            ICategoriesService categoriesService,
            ITherapistsService therapistsService,
            ITherapistsServicesService therapistsServicesService)
        {
            _mapper = mapper;
            _servicesService = servicesService;
            _cloudinaryService = cloudinaryService;
            _categoriesService = categoriesService;
            _therapistsService = therapistsService;
            _therapistsServicesService = therapistsServicesService;
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

        public async Task<IActionResult> CreateService()
        {
            var therapists = await _therapistsService.GetAllAsync();
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");

            var categories = await _categoriesService.GetAllAsync();
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            string imageUrlCloudinary = await _cloudinaryService.UploadFileAsync(input.Image, input.Name);

            var serviceToAdd = _mapper.Map<Service>(input);
            serviceToAdd.ImageUrl = imageUrlCloudinary;
            await _servicesService.AddAsync(serviceToAdd);
            var therapists = await _therapistsService.GetAllAsync();
            await _therapistsServicesService.AddAllTherapistsToServiceId(therapists, serviceToAdd.Id);

            TempData["SuccessfullyAdded"] = "You have successfully added the service!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditService(Guid id)
        {
            var categories = await _categoriesService.GetAllAsync();
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");

            var therapists = await _therapistsService.GetAllAsync();
            var therapistViewModel = _mapper.Map<IEnumerable<TherapistViewModel>>(therapists);
            ViewData["Therapists"] = therapistViewModel;

            var serviceToEdit = await _servicesService.GetByIdAsync(id);
            var viewModel = _mapper.Map<ServiceEditViewModel>(serviceToEdit);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceEditViewModel input)

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

            var serviceToEdit = await _servicesService.GetByIdAsync(input.Id);

            var result = _mapper.Map(input, serviceToEdit);

            await _servicesService.UpdateDetailsAsync(result);

            TempData["SuccessfullyEdited"] = "You have successfully edited the service!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var serviceToDelete = await _servicesService.GetByIdAsync(id);

            var viewModel = _mapper.Map<ServiceViewModel>(serviceToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, string imageUrl)
        {
            await _servicesService.DeleteAsync(id);
            await _cloudinaryService.DeleteFileAsync(imageUrl);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the service!";

            return RedirectToAction("Index");
        }
    }
}