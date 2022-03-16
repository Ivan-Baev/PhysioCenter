namespace PhysioCenter.Core.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AppointmentInputViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string TherapistId { get; set; }

        [Required]
        public string ServiceId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}