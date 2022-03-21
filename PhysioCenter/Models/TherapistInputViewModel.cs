namespace PhysioCenter.Models
{
    using System.ComponentModel.DataAnnotations;
    public class TherapistInputViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string ProfileImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public string? UserId { get; set; }
    }
}