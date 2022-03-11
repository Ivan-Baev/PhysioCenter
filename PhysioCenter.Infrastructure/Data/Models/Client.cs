namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public virtual string UserId { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<Therapist> Therapists { get; set; } = new HashSet<Therapist>();
    }
}