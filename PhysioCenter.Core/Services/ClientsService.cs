namespace PhysioCenter.Core.Services.Clients
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class ClientsService : IClientsService
    {
        private readonly IApplicationDbRepository repo;

        public ClientsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
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

        public async Task<Client> GetClientByUserId(string id)
        {
            return await repo.All<Client>()
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
        }
    }
}