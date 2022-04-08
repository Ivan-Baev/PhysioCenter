namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System.Threading.Tasks;

    public class TherapistsServicesService : ITherapistsServicesService
    {
        private readonly IApplicationDbRepository repo;

        public TherapistsServicesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAllServicesToTherapistId(IEnumerable<Service> services, Guid therapistId)
        {
            var therapistServices = new List<TherapistService>();

            foreach (var service in services)
            {
                TherapistService therapistService = new()
                {
                    TherapistId = therapistId,
                    ServiceId = service.Id,
                    isProvided = true,
                };

                therapistServices.Add(therapistService);
            }
            await repo.AddRangeAsync(therapistServices);
            await repo.SaveChangesAsync();
        }

        public async Task AddAllTherapistsToServiceId(IEnumerable<Therapist> therapists, Guid serviceId)
        {
            var serviceTherapists = new List<TherapistService>();

            foreach (var therapist in therapists)
            {
                TherapistService therapistService = new()
                {
                    TherapistId = therapist.Id,
                    ServiceId = serviceId,
                    isProvided = true,
                };

                serviceTherapists.Add(therapistService);
            }
            await repo.AddRangeAsync(serviceTherapists);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(string therapistId)
        {
            return await repo.All<TherapistService>()
                .Include(x => x.Service)
                .Where(x => x.TherapistId.ToString() == therapistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TherapistService>> GetProvidedTherapistServicesByIdAsync(string therapistId)
        {
            return await repo.All<TherapistService>()
                .Include(x => x.Service)
                .Where(x => x.TherapistId.ToString() == therapistId && x.isProvided)
                .ToListAsync();
        }

        public async Task ChangeProvidedStatusAsync(string therapistId, string serviceId)
        {
            var therapistService = await repo.All<TherapistService>()
                                .FirstOrDefaultAsync(
                                x => x.ServiceId.ToString() == serviceId
                                && x.TherapistId.ToString() == therapistId);

            therapistService.isProvided = !therapistService.isProvided;

            await repo.SaveChangesAsync();
        }

        public async Task AddTherapistToServiceAsync(string therapistId, string serviceId)
        {
            var therapistService = await repo.All<TherapistService>()
                                .FirstOrDefaultAsync(
                                x => x.ServiceId.ToString() == serviceId
                                && x.TherapistId.ToString() == therapistId);

            therapistService.isProvided = !therapistService.isProvided;

            await repo.SaveChangesAsync();
        }
    }
}