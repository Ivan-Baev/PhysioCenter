namespace PhysioCenter.Infrastructure.Data.Constants
{
    public static class DataValidations
    {
        public const int BlogTitleMinLength = 10;
        public const int BlogTitleMaxLength = 100;

        public const int BlogContentMinLength = 200;
        public const int BlogContentMaxLength = 2000;

        public const int CategoryNameMinLength = 10;
        public const int CategoryNameMaxLength = 100;

        public const int CategoryDescriptionMinLength = 100;
        public const int CategoryDescriptionMaxLength = 1000;

        public const int ClientFullNameMinLength = 4;
        public const int ClientFullNameMaxLength = 50;

        public const int NoteContentMinLength = 10;
        public const int NoteContentMaxLength = 200;

        public const int ReviewContentMinLength = 40;
        public const int ReviewContentMaxLength = 400;

        public const int ServiceDescriptionMinLength = 50;
        public const int ServiceDescriptionMaxLength = 500;

        public const int ServiceNameMinLength = 5;
        public const int ServiceNameMaxLength = 100;

        public const double ServiceMinPrice = 10;
        public const double ServiceMaxPrice = 70;

        public const int TherapistNameMinLength = 4;
        public const int TherapistNameMaxLength = 100;

        public const int TherapistDescriptionMinLength = 50;
        public const int TherapistDescriptionMaxLength = 1000;
    }
}