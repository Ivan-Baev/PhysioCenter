namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
