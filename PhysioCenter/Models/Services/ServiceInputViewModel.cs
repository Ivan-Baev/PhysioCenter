namespace PhysioCenter.Models.Services
{
    using PhysioCenter.Common;
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class ServiceInputViewModel
    {
        [Required]
        [StringLength(ServiceDescriptionMaxLength, MinimumLength = ServiceDescriptionMinLength, ErrorMessage = ErrorMessages.ServiceDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [StringLength(ServiceNameMaxLength, MinimumLength = ServiceNameMinLength, ErrorMessage = ErrorMessages.ServiceNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(ServiceMinPrice, ServiceMaxPrice, ErrorMessage = ErrorMessages.ServicePriceRange)]
        public double Price { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }
    }
}