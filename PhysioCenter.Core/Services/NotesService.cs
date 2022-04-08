namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class NotesService : INotesService
    {
        private readonly IApplicationDbRepository repo;

        public NotesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAsync(Note input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var note =
               await repo.All<Note>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Note>(note);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllByClientIdAsync(string clientId)
        {
            return
                 await repo.All<Note>()
                 .Where(x => x.ClientId == Guid.Parse(clientId))
                 .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task<Note> GetByIdAsync(string id)
        {
            return
                await repo.All<Note>()
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Note input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }
    }
}