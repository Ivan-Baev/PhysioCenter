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

        public ClientsService(ApplicationDbContext data, IHtmlSanitizer htmlSanitizer,
            IApplicationDbRepository _repo)
        {
            _htmlSanitizer = htmlSanitizer;
            repo = _repo;
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            return
                await repo.All<Client>()
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
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

            await repo.AddAsync(new Client
            {
                Id = Guid.NewGuid(),
                FullName = _htmlSanitizer.Sanitize(input.FullName),
            });
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
    }
}