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

        public async Task<IEnumerable<TherapistClient>> GetProvidedTherapistClientsByIdAsync(Guid therapistId)
        {
            return await repo.All<TherapistClient>()
                .Include(x => x.Client)
                .Where(x => x.TherapistId == therapistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TherapistClient>> GetProvidedClientTherapistsByIdAsync(string clientId)
        {
            return await repo.All<TherapistClient>()
                .Where(x => x.ClientId.ToString() == clientId)
                .Include(x => x.Therapist).ThenInclude(x => x.Appointments)
                .ToListAsync();
        }

        public async Task AddTherapistClientAsync(TherapistClient input)
        {
            var test = repo.All<TherapistClient>()
                .FirstOrDefaultAsync(x => x.ClientId == input.ClientId & x.TherapistId == input.TherapistId);

            if (test.Result == null)
            {
                await repo.AddAsync(input);
                await repo.SaveChangesAsync();
            }
        }
    }
}