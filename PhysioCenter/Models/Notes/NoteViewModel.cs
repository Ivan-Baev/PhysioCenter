namespace PhysioCenter.Models.Notes
{
    using System;

    public class NoteViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public virtual Guid ClientId { get; set; }
        public virtual Guid TherapistId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}