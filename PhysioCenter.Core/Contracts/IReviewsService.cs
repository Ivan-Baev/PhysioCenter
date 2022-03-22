namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetAllAsync();
    }
}
