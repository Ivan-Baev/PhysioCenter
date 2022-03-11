namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Therapist
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ProfileImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserId { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual ICollection<TherapistService> Services { get; set; } = new HashSet<TherapistService>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
        public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();
    }
}