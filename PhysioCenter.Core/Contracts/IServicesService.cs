namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IServicesService
    {
        Task<Service> GetByIdAsync(string id);

        Task<IEnumerable<Service>> GetAllAsync();

        Task AddAsync(Service input);

        Task DeleteAsync(string id);

        Task UpdateDetailsAsync(Service input);
    }
}