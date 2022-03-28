namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Therapists;
    using PhysioCenter.Models.TherapistsServices;

    public class TherapistsController : AdminController
    {
        private readonly ITherapistsService _therapistsService;
        private readonly IServicesService _servicesService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _usersManager;
        private readonly ICloudinaryService _cloudinaryService;

        public TherapistsController(
            ITherapistsService therapistsService,
            IServicesService servicesService,
            IMapper mapper,
            UserManager<IdentityUser> usersManager,
            ITherapistsServicesService therapistsServicesService,
            ICloudinaryService cloudinaryService)
        {
            _therapistsService = therapistsService;
            _servicesService = servicesService;
            _mapper = mapper;
            _usersManager = usersManager;
            _therapistsServicesService = therapistsServicesService;
            _cloudinaryService = cloudinaryService;
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

        public IActionResult CreateTherapist()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTherapist(TherapistInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await GenerateTherapistAccountAndPassword(input);

            string imageUrlCloudinary = await _cloudinaryService.UploadFileAsync(input.Image, input.FullName);

            var therapist = _mapper.Map<Therapist>(input);
            therapist.ProfileImageUrl = imageUrlCloudinary;

            await _therapistsService.AddAsync(therapist);

            var services = await _servicesService.GetAllAsync();

            await _therapistsServicesService.AddAllServicesToTherapistId(services, therapist.Id);

            TempData["SuccessfullyAdded"] = "You have successfully added a new therapist!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditTherapist(string id)
        {
            var services = await _therapistsServicesService.GetTherapistServicesByIdAsync(id);
            var therapistServices = _mapper.Map<IEnumerable<TherapistServiceViewModel>>(services);

            ViewData["Services"] = therapistServices;

            var therapistToEdit = await _therapistsService.GetByIdAsync(id);
            var viewModel = _mapper.Map<TherapistEditViewModel>(therapistToEdit);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTherapist(TherapistEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            if (input.Image != null)
            {
                await _cloudinaryService.DeleteFileAsync(input.ProfileImageUrl);
                input.ProfileImageUrl = await _cloudinaryService.UploadFileAsync(input.Image, input.FullName);
            }

            var therapistToEdit = await _therapistsService.GetByIdAsync(input.Id);

            var result = _mapper.Map(input, therapistToEdit);

            await _therapistsService.UpdateDetailsAsync(result);

            TempData["SuccessfullyEdited"] = "You have successfully edited the therapist!";

            return this.RedirectToAction(nameof(Index));
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

        private async Task GenerateTherapistAccountAndPassword(TherapistInputViewModel input)
        {
            IdentityUser identityUser = new($"{input.FullName}@physiocenter.com")
            {
                Email = $"{input.FullName}@physiocenter.com"
            };
            var user = await _usersManager.CreateAsync(identityUser, "randompassword");
            if (user.Succeeded)
            {
                await _usersManager.AddToRoleAsync(identityUser, "Therapist");
                input.UserId = identityUser.Id;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTherapistServiceProvidedStatus(string therapistId, string serviceId)
        {
            await this._therapistsServicesService.ChangeProvidedStatusAsync(therapistId, serviceId);

            return this.RedirectToAction("EditTherapist", "Therapists", new { id = therapistId });
        }
    }
}