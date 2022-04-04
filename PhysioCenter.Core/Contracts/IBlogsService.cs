namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IBlogsService
    {
        Task<IEnumerable<Blog>> GetAllAsync();

        Task<IEnumerable<Blog>> GetFeatured();

        Task AddAsync(Blog input);

        Task<Blog> GetByIdAsync(string id);

        Task UpdateDetailsAsync(Blog input);

        Task DeleteAsync(string id);
    }
}