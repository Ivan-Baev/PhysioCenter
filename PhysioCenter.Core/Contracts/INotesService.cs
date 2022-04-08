namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface INotesService
    {
        Task AddAsync(Note input);

        Task<Note> GetByIdAsync(Guid id);

        Task<IEnumerable<Note>> GetAllByClientIdAsync(string clientId);

        Task UpdateDetailsAsync(Note input);

        Task DeleteAsync(Guid id);
    }
}