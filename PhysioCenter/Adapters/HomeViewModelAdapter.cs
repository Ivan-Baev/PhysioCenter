namespace PhysioCenter.Adapters
{
    using AutoMapper;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Blogs;
    using PhysioCenter.Models.Home;
    using PhysioCenter.Models.Reviews;

    public class HomeViewModelAdapter : IHomeViewModelAdapter
    {
        private readonly IMapper _mapper;

        public HomeViewModelAdapter(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public HomeViewModel CreateHomeViewModel(IEnumerable<Blog> blogs, IEnumerable<Review> reviews)
        {
            return new HomeViewModel
            {
                Blogs = _mapper.Map<List<BlogViewModel>>(blogs),
                Reviews = _mapper.Map<List<ReviewViewModel>>(reviews)
            };
        }
    }
}