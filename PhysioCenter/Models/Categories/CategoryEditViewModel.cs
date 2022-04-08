namespace PhysioCenter.Models.Categories
{
    using PhysioCenter.Constants;
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class CategoryEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = ErrorMessages.CategoryNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(CategoryDescriptionMaxLength, MinimumLength = CategoryDescriptionMinLength, ErrorMessage = ErrorMessages.CategoryDescriptionLength)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        public IFormFile? Image { get; set; }
    }
}