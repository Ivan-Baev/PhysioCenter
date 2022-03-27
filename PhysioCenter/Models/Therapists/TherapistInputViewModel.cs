﻿namespace PhysioCenter.Models.Therapists
{
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    public class TherapistInputViewModel
    {
        [Required]
        public string FullName { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(3 * 1024 * 1024)]
        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Image { get; set; }

        [Required]
        public string Description { get; set; }

        public string? UserId { get; set; }
    }
}