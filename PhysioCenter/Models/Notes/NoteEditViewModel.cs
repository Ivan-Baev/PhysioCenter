﻿namespace PhysioCenter.Models.Notes
{
    using PhysioCenter.Constants;

    using System.ComponentModel.DataAnnotations;

    using static PhysioCenter.Infrastructure.Data.Constants.DataValidations;

    public class NoteEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(NoteContentMaxLength, MinimumLength = NoteContentMinLength, ErrorMessage = ErrorMessages.NoteContentLength)]
        public string Content { get; set; }

        [Required]
        public virtual Guid ClientId { get; set; }

        [Required]
        public virtual Guid TherapistId { get; set; }
    }
}