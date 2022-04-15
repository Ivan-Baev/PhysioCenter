namespace PhysioCenter.Models.Blogs
{
    using PhysioCenter.Common;
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class BlogInputViewModel
    {
        [Required]
        [StringLength(BlogTitleMaxLength, MinimumLength = BlogTitleMinLength, ErrorMessage = ErrorMessages.BlogTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(BlogContentMaxLength, MinimumLength = BlogContentMinLength, ErrorMessage = ErrorMessages.BlogContentLength)]
        public string Content { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = ErrorMessages.UploadImage)]
        public IFormFile Image { get; set; }
    }
}