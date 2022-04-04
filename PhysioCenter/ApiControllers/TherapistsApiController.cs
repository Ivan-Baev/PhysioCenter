namespace PhysioCenter.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Newtonsoft.Json;

    using PhysioCenter.Core.Contracts;

    /// <summary>
    /// Therapist Controller with Get functions
    /// </summary>
    [ApiController]
    [Route("api/[action]")]
    public class TherapistsApiController : ControllerBase
    {
        private readonly ILogger<TherapistsApiController> _logger;
        private readonly IAppointmentsService _appointmentsService;
        private readonly ITherapistsServicesService _therapistsServicesService;

        /// <summary>
        /// random
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="appointmentsService"></param>
        /// <param name="therapistsServicesService"></param>
        public TherapistsApiController(ILogger<TherapistsApiController> logger,
            IAppointmentsService appointmentsService,
            ITherapistsServicesService therapistsServicesService)
        {
            _logger = logger;
            _appointmentsService = appointmentsService;
            _therapistsServicesService = therapistsServicesService;
        }

        /// <summary>
        /// Get free booking hours from therapist schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///
        [HttpGet(Name = "GetTherapistSchedule")]
        public async Task<JsonResult> GetTherapistSchedule(string id)
        {
            var items = await _appointmentsService.GetUpcomingByTherapistIdAsync(id);

            var schedule = new List<string>();
            foreach (var appointment in items)
            {
                schedule.Add(appointment.DateTime.ToString("dd/MM/yyyy H"));
            }
            var json = JsonConvert.SerializeObject(schedule);

            return new JsonResult(json);
        }

        /// <summary>
        /// Gets all services provided currently by the therapist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetTherapistServices")]
        public async Task<ActionResult> GetTherapistServices(string id)
        {
            var services = await _therapistsServicesService.GetProvidedTherapistServicesByIdAsync(id);
            var providedServices = new SelectList(services, "ServiceId", "Service.Name");

            return new JsonResult(providedServices);
        }
    }
}