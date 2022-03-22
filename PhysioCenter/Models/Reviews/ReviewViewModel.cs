namespace PhysioCenter.Models.Reviews
{

    using System;

    public class ReviewViewModel
    {
        public Guid Id;
        public string Content { get; set; }

        public Guid ClientId { get; set; }

        public string ClientFullName { get; set; }

        public Guid TherapistId { get; set; }

        public string TherapistFullName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}