namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Models;
    using PhysioCenter.Models.Therapists;
    using PhysioCenter.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TherapistsController : AdminController
    {
        private readonly ITherapistsService _therapistsService;
        private readonly IServicesService _servicesService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _usersManager;

        public TherapistsController(
            ITherapistsService therapistsService,
            IServicesService servicesService,
            IMapper mapper,
            UserManager<IdentityUser> usersManager, ITherapistsServicesService therapistsServicesService)
        {
            _therapistsService = therapistsService;
            _servicesService = servicesService;
            _mapper = mapper;
            _usersManager = usersManager;
            _therapistsServicesService = therapistsServicesService;
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

            var therapist = _mapper.Map<Therapist>(input);
            await _therapistsService.AddAsync(therapist);
            var servicesIds = await _servicesService.GetAllAsync();
            var therapistServices = new List<TherapistService>();
            
            foreach (var service in servicesIds)
            {
                TherapistService therapistService = new()
                    {
                     TherapistId = therapist.Id,
                     ServiceId = service.Id,
                     isProvided = true,
                    };

                therapistServices.Add(therapistService);
            }


            await _therapistsServicesService.AddTherapistServiceAsync(therapistServices);


            TempData["SuccessfullyAdded"] = "You have successfully added a new therapist!";

            return this.RedirectToAction(nameof(Index));
        }


        //public async Task<IActionResult> EditTherapist(string id)
        //{
        //    var appointmentToEdit = await _appointmentsService.GetByIdAsync(id);
        //    var viewModel = _mapper.Map<AppointmentInputViewModel>(appointmentToEdit);


        //    var clients = await _clientsService.GetAllAsync();
        //    var therapists = await _therapistsService.GetAllAsync();
        //    var services = await _servicesService.GetAllAsync();

        //    ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
        //    ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
        //    ViewData["Services"] = new SelectList(services, "Id", "Name");

        //    var hoursToDisable = await GetTherapistSchedule(appointmentToEdit.TherapistId.ToString());
        //    ViewData["hoursToDisable"] = hoursToDisable.Value;

        //    return this.View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditTherapist(AppointmentInputViewModel input, string id)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(input);
        //    }

        //    var appointment = mapper.Map<Appointment>(input);
        //    appointment.Id = Guid.Parse(id);
        //    await _appointmentsService.UpdateAsync(appointment);

        //    TempData["SuccessfullyEdited"] = "You have successfully edited the appointment!";

        //    return this.RedirectToAction(nameof(Index));
        //}

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
                user = await _usersManager.AddToRoleAsync(identityUser, "Therapist");
                input.UserId = identityUser.Id;
            }
        }
    }
}
