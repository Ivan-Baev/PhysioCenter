namespace PhysioCenter.Models.Home
{
    using PhysioCenter.Models.Blogs;
    using PhysioCenter.Models.Reviews;

    public class HomeViewModel
    {
        public List<BlogViewModel> Blogs { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}