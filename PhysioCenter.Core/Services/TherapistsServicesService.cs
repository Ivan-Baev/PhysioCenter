namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System.Threading.Tasks;

    public class TherapistsServicesService : ITherapistsServicesService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ITherapistsService _therapistsService;
        private readonly IServicesService _servicesService;

        public TherapistsServicesService(
            IApplicationDbRepository _repo,
            ITherapistsService therapistsService,
            IServicesService servicesService)
        {
            repo = _repo;
            _therapistsService = therapistsService;
            _servicesService = servicesService;
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

        public async Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(Guid therapistId)
        {
            await _therapistsService.FindTherapistById(therapistId);

            return await repo.All<TherapistService>()
                .Include(x => x.Service)
                .Where(x => x.TherapistId == therapistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TherapistService>> GetProvidedTherapistServicesByIdAsync(Guid therapistId)
        {
            await _therapistsService.FindTherapistById(therapistId);

            return await repo.All<TherapistService>()
                .Include(x => x.Service)
                .Where(x => x.TherapistId == therapistId && x.isProvided)
                .ToListAsync();
        }

        public async Task ChangeProvidedStatusAsync(Guid therapistId, Guid serviceId)
        {
            await _therapistsService.FindTherapistById(therapistId);
            await _servicesService.GetByIdAsync(serviceId);

            var therapistService = await repo.All<TherapistService>()
                                .FirstOrDefaultAsync(
                                x => x.ServiceId == serviceId
                                && x.TherapistId == therapistId);

            if (therapistService == null)
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.InvalidTherapistService);
            }
            therapistService.isProvided = !therapistService.isProvided;

            await repo.SaveChangesAsync();
        }

        public async Task<TherapistService> FindTherapistServiceById(Guid therapistId, Guid serviceId)
        {
            await _therapistsService.FindTherapistById(therapistId);
            await _servicesService.GetByIdAsync(serviceId);

            var therapistService = await repo.All<TherapistService>()
                                .FirstOrDefaultAsync(
                                x => x.ServiceId == serviceId
                                && x.TherapistId == therapistId
                                && x.isProvided);

            if (therapistService == null)
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.ServiceNotProvidedByTherapist);
            }

            return therapistService;
        }
    }
}