namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class Therapist
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TherapistNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [Url]
        public string ProfileImageUrl { get; set; }

        [Required]
        [MaxLength(TherapistDescriptionMaxLength)]
        public string Description { get; set; }

        public virtual string UserId { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual ICollection<TherapistService> Services { get; set; } = new HashSet<TherapistService>();
        public virtual ICollection<TherapistClient> Clients { get; set; } = new HashSet<TherapistClient>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Note> Notes { get; set; } = new HashSet<Note>();
    }
}