namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TherapistService
    {
        [Required]
        public Guid TherapistId { get; set; }

        public virtual Therapist Therapist { get; set; }

        public Guid ServiceId { get; set; }

        public virtual Service Service { get; set; }

        public bool isProvided { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}