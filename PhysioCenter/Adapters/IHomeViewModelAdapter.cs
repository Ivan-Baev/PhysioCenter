namespace PhysioCenter.Adapters
{
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Models.Home;

    public interface IHomeViewModelAdapter
    {
        HomeViewModel CreateHomeViewModel(IEnumerable<Blog> blogs, IEnumerable<Review> reviews);
    }
}