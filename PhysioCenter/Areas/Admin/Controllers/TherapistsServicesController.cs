namespace PhysioCenter.Areas.Admin.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using PhysioCenter.Areas.Administration.Controllers;
    using PhysioCenter.Core.Contracts;

    public class TherapistsServicesController : AdminController
    {
        private readonly ITherapistsServicesService _therapistsServicesService;

        public TherapistsServicesController(
            ITherapistsServicesService therapistsServicesService)
        {
            _therapistsServicesService = therapistsServicesService;
        }

        [HttpPost]
        public async Task<IActionResult> EditTherapistServiceProvidedStatus(string therapistId, string serviceId)
        {
            await this._therapistsServicesService.ChangeProvidedStatusAsync(therapistId, serviceId);

            return this.RedirectToAction("EditTherapist", "Therapists", new { id = therapistId });
        }
    }
}