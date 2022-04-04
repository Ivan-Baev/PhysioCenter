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
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "Therapist")]
        public string TherapistId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public string ServiceId { get; set; }


        [Required]
        [Display(Name = "Time")]
        [AllowedDateTime]
        public DateTime DateTime { get; set; }
    }
}