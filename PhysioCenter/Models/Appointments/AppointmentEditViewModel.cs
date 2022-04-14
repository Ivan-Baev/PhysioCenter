namespace PhysioCenter.Models.Appointments
{
    using PhysioCenter.CustomAttributes.DateTimeParser;

    using System.ComponentModel.DataAnnotations;

    public class AppointmentEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:00}")]
        [AllowedDateTime]
        public DateTime DateTime { get; set; }
    }
}