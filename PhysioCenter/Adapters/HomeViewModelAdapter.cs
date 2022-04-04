namespace PhysioCenter.Adapters
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Blogs;
    using PhysioCenter.Models.Home;
    using PhysioCenter.Models.Reviews;

    public class HomeViewModelAdapter : IHomeViewModelAdapter
    {
        private readonly IMapper mapper;

        public HomeViewModelAdapter(
            IMapper mapper)
        {
            this.mapper = mapper;
        }

        public HomeViewModel CreateHomeViewModel(IEnumerable<Blog> blogs, IEnumerable<Review> reviews)
        {
            return new HomeViewModel
            {
                Blogs = mapper.Map<List<BlogViewModel>>(blogs),
                Reviews = mapper.Map<List<ReviewViewModel>>(reviews)
            };
        }
    }
}