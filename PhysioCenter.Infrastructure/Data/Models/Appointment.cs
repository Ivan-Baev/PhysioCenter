namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        public Guid TherapistId { get; set; }

        public Therapist Therapist { get; set; }

        [Required]
        public virtual TherapistService TherapistService { get; set; }

        public DateTime DateTime { get; set; }
    }
}