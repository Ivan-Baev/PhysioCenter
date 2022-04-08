namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task AddAsync(Category input);

        Task<Category> GetByIdAsync(Guid id);

        Task UpdateDetailsAsync(Category input);

        Task DeleteAsync(Guid id);
    }
}