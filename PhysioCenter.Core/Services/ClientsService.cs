namespace PhysioCenter.Core.Services.Clients
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class ClientsService : IClientsService
    {
        private readonly ApplicationDbContext _data;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public ClientsService(ApplicationDbContext data, IHtmlSanitizer htmlSanitizer)
        {
            _data = data;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<Client> GetByIdAsync(string id)
        {
            var client =
                await _data.Clients
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var clients =
                await _data.Clients
                .OrderByDescending(x => x.FullName)
                .ToListAsync();
            return clients;
        }

        public async Task AddAsync(Client input)
        {
            if (_data.Clients.Any(c => c.FullName == input.FullName))
                return;

            await _data.Clients.AddAsync(new Client
            {
                Id = Guid.NewGuid(),
                FullName = _htmlSanitizer.Sanitize(input.FullName),
            });
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var client =
               await _data.Clients
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Clients.Remove(client);
            await _data.SaveChangesAsync();
        }
    }
}