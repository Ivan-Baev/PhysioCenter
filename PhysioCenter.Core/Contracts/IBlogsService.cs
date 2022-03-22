namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IBlogsService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
    }
}
