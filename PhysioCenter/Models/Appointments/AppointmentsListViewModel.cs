namespace PhysioCenter.Models.Appointments
{
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
        public int PageCount { get; set; }

        public int CurrentPage { get; set; }
    }
}