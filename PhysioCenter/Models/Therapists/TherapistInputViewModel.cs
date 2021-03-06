namespace PhysioCenter.Models.Therapists
{
    using PhysioCenter.CustomAttributes.Images;
    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    using System.ComponentModel.DataAnnotations;
    using PhysioCenter.Common;

    public class TherapistInputViewModel
    {
        [Required]
        [StringLength(TherapistNameMaxLength, MinimumLength = TherapistNameMinLength, ErrorMessage = ErrorMessages.TherapistNameLength)]
        public string FullName { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(TherapistDescriptionMaxLength, MinimumLength = TherapistDescriptionMinLength, ErrorMessage = ErrorMessages.TherapistDescriptionLength)]
        public string Description { get; set; }

        public string? UserId { get; set; }
    }
}