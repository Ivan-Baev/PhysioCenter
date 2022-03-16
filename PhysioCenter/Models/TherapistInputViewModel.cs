namespace PhysioCenter.Core.InputModels
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
    }
}