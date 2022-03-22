namespace PhysioCenter.Models.Reviews
{

    using System.Collections.Generic;

    public class ReviewsListViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}