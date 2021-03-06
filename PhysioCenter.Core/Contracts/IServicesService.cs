namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IServicesService
    {
        Task<Service> GetByIdAsync(Guid id);

        Task<IEnumerable<Service>> GetAllAsync();

        Task AddAsync(Service input);

        Task DeleteAsync(Guid id);

        Task UpdateDetailsAsync(Service input);
    }
}