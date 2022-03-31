namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface IClientsService
    {
        Task<IEnumerable<Client>> GetAllByIdAsync(string id);

        Task<IEnumerable<Client>> GetAllAsync();

        Task<Client> GetClientByUserId(string id);

        Task AddAsync(Client input);

        Task DeleteAsync(string id);
    }
}