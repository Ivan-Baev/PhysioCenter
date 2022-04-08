namespace PhysioCenter.Models.Appointments
{
    using System.Collections.Generic;

    public class AppointmentsListViewModel : PagingViewModel
    {
        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
    }
}