﻿namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Newtonsoft.Json;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Appointments;

    using System.Threading.Tasks;

    public class AppointmentsController : AdminController
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IServicesService _servicesService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsService _therapistsService;
        private readonly ITherapistsServicesService _therapistsServicesService;
        private readonly IMapper mapper;

        public AppointmentsController(IAppointmentsService appointmentsService,
            IServicesService servicesService,
            IClientsService clientsService,
            ITherapistsService therapistsService,
            IMapper mapper,
            ITherapistsServicesService therapistsServicesService)
        {
            _appointmentsService = appointmentsService;
            _servicesService = servicesService;
            _clientsService = clientsService;
            _therapistsService = therapistsService;
            this.mapper = mapper;
            _therapistsServicesService = therapistsServicesService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 10;
            var input = await _appointmentsService.GetAllAsync();
            var viewModel = new AppointmentsListViewModel
            {
                Appointments = mapper.Map<IEnumerable<AppointmentViewModel>>(input).Skip((page - 1) * pageSize).Take(pageSize)
            };
            var count = input.Count();
            viewModel.CurrentPage = page;
            viewModel.PageCount = (int)Math.Ceiling((double)count / pageSize) != 0
                ? (int)Math.Ceiling((double)count / pageSize) : 1;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            var clients = await _clientsService.GetAllAsync();
            var therapists = await _therapistsService.GetAllAsync();
            var services = await _servicesService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = mapper.Map<Appointment>(input);
            await _appointmentsService.AddAsync(appointment);

            TempData["SuccessfullyAdded"] = "You have successfully created a new appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditAppointment(string id)
        {
            var appointmentToEdit = await _appointmentsService.GetByIdAsync(id);
            var viewModel = mapper.Map<AppointmentEditViewModel>(appointmentToEdit);

            var clients = await _clientsService.GetAllAsync();
            var therapists = await _therapistsService.GetAllAsync();
            var services = await _servicesService.GetAllAsync();

            ViewData["Clients"] = new SelectList(clients, "Id", "FullName");
            ViewData["Therapists"] = new SelectList(therapists, "Id", "FullName");
            ViewData["Services"] = new SelectList(services, "Id", "Name");

            var hoursToDisable = await GetTherapistSchedule(appointmentToEdit.TherapistId.ToString());
            ViewData["hoursToDisable"] = hoursToDisable.Value;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(AppointmentEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var appointment = mapper.Map<Appointment>(input);
            await _appointmentsService.UpdateAsync(appointment);

            TempData["SuccessfullyEdited"] = "You have successfully edited the appointment!";

            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var appointmentToDelete = await _appointmentsService.GetByIdAsync(id);

            var viewModel = mapper.Map<AppointmentViewModel>(appointmentToDelete);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _appointmentsService.DeleteAsync(id);

            TempData["SuccessfullyDeleted"] = "You have successfully deleted the appointment!";

            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetTherapistSchedule(string id)
        {
            var items = await _appointmentsService.GetUpcomingByTherapistIdAsync(id);

            var schedule = new List<string>();
            foreach (var appointment in items)
            {
                schedule.Add(appointment.DateTime.ToString("dd/MM/yyyy HH"));
            }
            var json = JsonConvert.SerializeObject(schedule);

            return Json(json);
        }

        public async Task<ActionResult> GetTherapistServices(string id)
        {
            var services = await _therapistsServicesService.GetProvidedTherapistServicesByIdAsync(id);
            var providedServices = new SelectList(services, "ServiceId", "Service.Name");

            return Json(providedServices);
        }
    }
}