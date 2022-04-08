namespace PhysioCenter.Models.Appointments
{
    using PhysioCenter.CustomAttributes.DateTimeParser;

    using System.ComponentModel.DataAnnotations;

    public class AppointmentInputViewModel
    {
        [Required]
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        [Required]
        [Display(Name = "Therapist")]
        public Guid TherapistId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public Guid ServiceId { get; set; }

        [Required]
        [Display(Name = "Time")]
        [AllowedDateTime]
        public DateTime DateTime { get; set; }
    }
}