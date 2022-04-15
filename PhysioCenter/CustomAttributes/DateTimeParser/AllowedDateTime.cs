namespace PhysioCenter.CustomAttributes.DateTimeParser
{
    using System.ComponentModel.DataAnnotations;

    public class AllowedDateTime : ValidationAttribute
    {
        private const int startShift = 9;
        private const int endShift = 19;
        private const int lunchBreak = 13;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddMonths(2).Date;

            if (dt >= startDate &&
                dt <= endDate &&
                dt.Hour >= startShift &&
                dt.Hour <= endShift &&
                dt.Hour != lunchBreak &&
                dt.Minute == 00 &&
                dt.Second == 00)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? Common.ErrorMessages.InvalidCalendarDate);
        }
    }
}