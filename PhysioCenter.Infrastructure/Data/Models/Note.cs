namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NoteContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}