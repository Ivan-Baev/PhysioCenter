namespace PhysioCenter.Core.Utilities.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidAppointmentId = "The provided id does not exist";

        public const string InvalidBlogId = "This blog does not exist";

        public const string DuplicateBlogTitle = "Blog title already exists";

        public const string InvalidCategoryId = "This category doesn't exist!";

        public const string DuplicateCategoryTitle = "This category name already exists";

        public const string InvalidClientId = "This client does not exist!";

        public const string InvalidNoteId = "This note does not exist";

        public const string InvalidReviewId = "This review does not exist!";

        public const string InvalidServiceId = "This service does not exist!";

        public const string DuplicateServiceName = "This service name already exists";

        public const string InvalidTherapistId = "This therapist does not exist!";

        public const string InvalidTherapistService = "This therapist does not provide the chosen service. Unable to disable.";

        public const string ServiceNotProvidedByTherapist = "This service is not provided by the therapist!";
    }
}