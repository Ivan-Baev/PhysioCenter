namespace PhysioCenter.Models.Therapists
{
    public class TherapistCardViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
    }
}