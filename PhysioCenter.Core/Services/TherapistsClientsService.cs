namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System.Threading.Tasks;

    public class TherapistsClientsService : ITherapistsClientsService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ITherapistsService _therapistsService;
        private readonly IClientsService _clientsService;

        public TherapistsClientsService(IApplicationDbRepository _repo,
            ITherapistsService therapistsService,
            IClientsService clientsService)
        {
            repo = _repo;
            _therapistsService = therapistsService;
            _clientsService = clientsService;
        }

        public async Task<IEnumerable<TherapistClient>> GetProvidedTherapistClientsByIdAsync(Guid therapistId)
        {
            await _therapistsService.FindTherapistById(therapistId);

            return await repo.All<TherapistClient>()
                .Include(x => x.Client)
                .Where(x => x.TherapistId == therapistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TherapistClient>> GetProvidedClientTherapistsByIdAsync(Guid clientId)
        {
            await _clientsService.FindClientById(clientId);

            return await repo.All<TherapistClient>()
                .Where(x => x.ClientId == clientId)
                .Include(x => x.Therapist).ThenInclude(x => x.Appointments)
                .ToListAsync();
        }

        public async Task AddTherapistClientAsync(TherapistClient input)
        {
            var test = await repo.All<TherapistClient>()
                .FirstOrDefaultAsync(x => x.ClientId == input.ClientId && x.TherapistId == input.TherapistId);

            if (test == null)
            {
                await repo.AddAsync(input);
                await repo.SaveChangesAsync();
            }
        }
    }
}