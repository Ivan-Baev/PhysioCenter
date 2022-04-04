namespace PhysioCenter.CustomAttributes.DateTimeParser
{
    using System.ComponentModel.DataAnnotations;

    public class AllowedDateTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddMonths(2).Date;
            if (dt >= startDate && dt <= endDate && dt.Hour >= 9 && dt.Hour <= 19 && dt.Hour != 13 && dt.Minute == 00 && dt.Second == 00)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Please select a valid date from the calendar!");
        }
    }
}