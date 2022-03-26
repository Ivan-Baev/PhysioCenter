namespace PhysioCenter.Models.Appointments
{
    using System;

    public class AppointmentViewModel
    {
        public string Id { get; set; }

        public DateTime DateTime { get; set; }

        public string ClientId { get; set; }

        public string ClientFullName { get; set; }

        public string TherapistId { get; set; }

        public string TherapistFullName { get; set; }

        public string ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}