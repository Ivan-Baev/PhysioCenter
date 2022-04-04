namespace PhysioCenter.Models.Reviews
{
    using PhysioCenter.Constants;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class ReviewInputViewModel
    {
        [Required]
        [StringLength(ReviewContentMaxLength, MinimumLength = ReviewContentMinLength, ErrorMessage = ErrorMessages.ReviewContentLength)]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}