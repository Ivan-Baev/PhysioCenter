namespace PhysioCenter.Core.Services
{
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
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task<Client> GetClientByUserId(string id)
        {
            return await repo.All<Client>()
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Client> FindClientById(Guid id)
        {
            var client = await repo.All<Client>()
               .FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.InvalidClientId);
            }

            return client;
        }
    }
}