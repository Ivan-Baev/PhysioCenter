namespace PhysioCenter.Models.Notes
{
    using System.ComponentModel.DataAnnotations;

    public class NoteEditViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }
    }
}