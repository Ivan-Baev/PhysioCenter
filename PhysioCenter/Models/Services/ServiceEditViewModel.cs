namespace PhysioCenter.Models.Services
{
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    public class ServiceEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "The description must be between {2} and {1} characters long!")]
        public string Description { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "The description must be between {2} and {1} characters long!")]
        public string Name { get; set; }

        [Required]
        [Range(10, 100, ErrorMessage = "Price must be between {2} and {1}!")]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        public IFormFile? Image { get; set; }
    }
}