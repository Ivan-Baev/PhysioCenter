﻿namespace PhysioCenter.Models.Blogs
{
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    public class BlogInputViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }
    }
}