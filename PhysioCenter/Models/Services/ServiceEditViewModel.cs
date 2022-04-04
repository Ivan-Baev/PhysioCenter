namespace PhysioCenter.Models.Services
{
    using PhysioCenter.Constants;
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class ServiceEditViewModel
    {
        public string Id { get; set; }

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

        [Url]
        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        public IFormFile? Image { get; set; }
    }
}