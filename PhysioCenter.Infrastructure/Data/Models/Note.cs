namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Content { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid TherapistId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}