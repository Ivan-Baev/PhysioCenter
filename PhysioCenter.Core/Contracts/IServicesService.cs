namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IServicesService
    {
        Task<Service> GetByIdAsync(string id);

        Task<IEnumerable<Service>> GetAllAsync();

        Task<IEnumerable<Service>> GetAllByTherapistIdAsync(Guid id);

        Task AddAsync(Service input);

        Task DeleteAsync(string id);

        Task UpdateDetailsAsync(Service input);
    }
}