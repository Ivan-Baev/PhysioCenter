namespace PhysioCenter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AppointmentInputViewModel
    {
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
        public DateTime DateTime { get; set; }
    }
}