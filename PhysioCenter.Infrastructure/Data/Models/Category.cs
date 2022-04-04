namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CategoryDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
    }
}