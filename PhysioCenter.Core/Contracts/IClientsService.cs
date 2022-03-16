namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IClientsService
    {
        Task<Client> GetByIdAsync(string id);

        Task<IEnumerable<Client>> GetAllAsync();

        Task AddAsync(Client input);

        Task DeleteAsync(string id);
    }
}