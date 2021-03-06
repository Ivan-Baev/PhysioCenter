namespace PhysioCenter.CustomAttributes.Images
{
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            if (file != null && file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return string.Format(Common.ErrorMessages.MaximumFileSizeError, _maxFileSize / (1024 * 1024));
        }
    }
}