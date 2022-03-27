namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface INotesService
    {
        Task<IEnumerable<Note>> GetAllAsync();

        Task AddAsync(Note input);

        Task<Note> GetByIdAsync(string id);

        Task<IEnumerable<Note>> GetAllByClientIdAsync(string clientId);

        Task UpdateDetailsAsync(Note input);

        Task DeleteAsync(string id);
    }
}