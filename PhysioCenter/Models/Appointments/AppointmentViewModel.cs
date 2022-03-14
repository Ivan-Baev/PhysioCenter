namespace PhysioCenter.Models.Appointments
{
    using PhysioCenter.Core.Mappings;
    using PhysioCenter.Infrastructure.Data.Models;

    using System;

    public class AppointmentViewModel : IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public DateTime DateTime { get; set; }

        public string ClientId { get; set; }

        public string ClientFirstName { get; set; }

        public string TherapistId { get; set; }

        public string TherapistFullName { get; set; }

        public string ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}