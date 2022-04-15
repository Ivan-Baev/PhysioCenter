namespace PhysioCenter.Models.Categories
{
    using PhysioCenter.Common;
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class CategoryInputViewModel
    {
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = ErrorMessages.CategoryNameLength)]
        public string Name { get; set; }

        [StringLength(CategoryDescriptionMaxLength, MinimumLength = CategoryDescriptionMinLength, ErrorMessage = ErrorMessages.CategoryDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = ErrorMessages.UploadImage)]
        public IFormFile Image { get; set; }
    }
}