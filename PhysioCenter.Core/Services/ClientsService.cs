namespace PhysioCenter.Core.Services.Clients
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class ClientsService : IClientsService
    {
        private readonly IApplicationDbRepository repo;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public ClientsService(IHtmlSanitizer htmlSanitizer,
            IApplicationDbRepository _repo)
        {
            _htmlSanitizer = htmlSanitizer;
            repo = _repo;
        }

        public async Task<IEnumerable<Client>> GetAllByIdAsync(string id)
        {
            return
                await repo.All<Client>()
                .Include(x => x.Therapists
                                    .Where(x => x.TherapistId == Guid.Parse(id)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return
                await repo.All<Client>()
                .OrderByDescending(x => x.FullName)
                .ToListAsync();
        }

        public async Task AddAsync(Client input)
        {
            if (repo.All<Client>().Any(c => c.FullName == input.FullName))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var client =
               await repo.All<Client>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Client>(client);
            await repo.SaveChangesAsync();
        }

        public async Task<Client> GetClientByUserId(string id)
        {
            return await repo.All<Client>()
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
        }
    }
}