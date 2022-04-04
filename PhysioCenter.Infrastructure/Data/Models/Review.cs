namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Review
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(ReviewContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        public Client Client { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }

        public Therapist Therapist { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}