namespace PhysioCenter.Infrastructure.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TherapistClient
    {
        [Required]
        public Guid TherapistId { get; set; }

        public virtual Therapist Therapist { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}