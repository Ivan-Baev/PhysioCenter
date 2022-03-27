namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class NotesService : INotesService
    {
        private readonly ApplicationDbContext _data;

        public NotesService(ApplicationDbContext data)
        {
            _data = data;
        }

        public async Task AddAsync(Note input)
        {
            await _data.Notes.AddAsync(input);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var blog =
               await _data.Notes
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Notes.Remove(blog);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            return await _data.Notes
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetAllByClientIdAsync(string clientId)
        {
            return
                 await _data.Notes
                 .Where(x => x.ClientId == Guid.Parse(clientId))
                .ToListAsync();
        }

        public async Task<Note> GetByIdAsync(string id)
        {
            return
                await _data.Notes
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Note input)
        {
            _data.Notes.Update(input);
            await _data.SaveChangesAsync();
        }
    }
}