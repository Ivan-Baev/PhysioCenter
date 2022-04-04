namespace PhysioCenter.Models.Therapists
{
    using System.Collections.Generic;

    public class TherapistsListViewModel
    {
        public IEnumerable<TherapistViewModel> Therapists { get; set; }
    }
}