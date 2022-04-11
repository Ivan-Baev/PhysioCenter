namespace PhysioCenter.Core.Services
{
    using PhysioCenter.Core.Contracts;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class NotesService : INotesService
    {
        private readonly IApplicationDbRepository repo;
        private readonly IClientsService _clientsService;

        public NotesService(IApplicationDbRepository _repo,
            IClientsService clientsService)
        {
            repo = _repo;
            _clientsService = clientsService;
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            var note = await repo.All<Note>()
                .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (note == null)
            {
                throw new ArgumentException("This note does not exist");
            }

            return note;
        }

        public async Task<IEnumerable<Note>> GetAllByClientIdAsync(Guid clientId)
        {
            await _clientsService.FindClientById(clientId);

            return
                 await repo.All<Note>()
                 .Where(x => x.ClientId == clientId)
                 .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task AddAsync(Note input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Note input)
        {
            var note = await GetByIdAsync(input.Id);

            note.Content = input.Content;

            repo.Update(note);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var note = await GetByIdAsync(id);

            repo.Delete(note);
            await repo.SaveChangesAsync();
        }
    }
}