namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System.Threading.Tasks;

    public class TherapistsClientsService : ITherapistsClientsService
    {
        private readonly IApplicationDbRepository repo;

        public TherapistsClientsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<TherapistClient>> GetProvidedTherapistClientsByIdAsync(string therapistId)
        {
            return await repo.All<TherapistClient>()
                .Include(x => x.Client)
                .Where(x => x.TherapistId.ToString() == therapistId)
                .ToListAsync();
        }

        public async Task AddTherapistClientAsync(TherapistClient input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }
    }
}