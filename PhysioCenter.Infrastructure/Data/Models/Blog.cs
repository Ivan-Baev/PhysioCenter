namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Blog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(BlogTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(BlogContentMaxLength)]
        public string Content { get; set; }
    }
}