namespace PhysioCenter.Core.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ServiceInputViewModel
    {
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

        [Required]
        [Url(ErrorMessage = "The provided url is invalid")]
        public string ImageUrl { get; set; }
    }
}