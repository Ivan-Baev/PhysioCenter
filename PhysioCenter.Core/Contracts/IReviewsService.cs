namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task AddAsync(Review input);

        Task DeleteAsync(string id);
    }
}