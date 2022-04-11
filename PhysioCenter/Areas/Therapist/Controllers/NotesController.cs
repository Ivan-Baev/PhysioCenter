namespace PhysioCenter.Areas.Therapist.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Notes;

    public class NotesController : TherapistController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly INotesService _notesService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public NotesController(IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            ITherapistsServicesService therapistsServicesService,
            UserManager<IdentityUser> userManager,
            INotesService notesService)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            _mapper = mapper;
            _therapistsServicesService = therapistsServicesService;
            _userManager = userManager;
            _notesService = notesService;
        }

        public async Task<IActionResult> Index(Guid clientId, Guid therapistId)
        {
            var input = await _notesService.GetAllByClientIdAsync(clientId);

            var viewModel = new NotesListViewModel
            {
                Notes = _mapper.Map<IEnumerable<NoteViewModel>>(input)
            };

            ViewBag.ClientId = clientId;
            ViewBag.TherapistId = therapistId;

            return View(viewModel);
        }

        public IActionResult CreateNote(Guid clientId, Guid therapistId)
        {
            ViewBag.ClientId = clientId;
            ViewBag.TherapistId = therapistId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return View(input);
            }

            var noteToAdd = _mapper.Map<Note>(input);
            await _notesService.AddAsync(noteToAdd);

            TempData["SuccessfullyAdded"] = "You have successfully created a new note!";

            return this.RedirectToAction(nameof(Index), new { clientId = input.ClientId.ToString(), therapistId = input.TherapistId.ToString() });
        }

        public async Task<IActionResult> EditNote(Guid id)
        {
            var noteToEdit = await _notesService.GetByIdAsync(id);

            var viewModel = _mapper.Map<NoteEditViewModel>(noteToEdit);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditNote(NoteEditViewModel input)

        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var noteToEdit = await _notesService.GetByIdAsync(input.Id);

            var result = _mapper.Map(input, noteToEdit);

            await _notesService.UpdateDetailsAsync(result);

            TempData["SuccessfullyEdited"] = "You have successfully edited the note!";

            return RedirectToAction(nameof(Index), new { clientId = input.ClientId.ToString(), therapistId = input.TherapistId.ToString() });
        }

        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var noteToDelete = await _notesService.GetByIdAsync(id);

            var viewModel = _mapper.Map<NoteViewModel>(noteToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid clientId, Guid therapistId)
        {
            await _notesService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the note!";

            return RedirectToAction(nameof(Index), new { clientId, therapistId });
        }
    }
}