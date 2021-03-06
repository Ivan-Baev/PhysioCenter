namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(ServiceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [MaxLength(ServiceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(ServiceMinPrice, ServiceMaxPrice)]
        public double Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<TherapistService> Therapists { get; set; }
    }
}