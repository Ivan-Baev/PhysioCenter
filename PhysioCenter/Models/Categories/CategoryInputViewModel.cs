namespace PhysioCenter.Models.Categories
{
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    public class CategoryInputViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }
    }
}