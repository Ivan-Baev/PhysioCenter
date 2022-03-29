namespace PhysioCenter.Models.Notes
{
    using PhysioCenter.CustomAttributes.Images;

    using System.ComponentModel.DataAnnotations;

    public class NoteInputViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}