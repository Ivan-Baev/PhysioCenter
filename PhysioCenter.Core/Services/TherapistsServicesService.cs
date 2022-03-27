namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Threading.Tasks;

    public class TherapistsServicesService : ITherapistsServicesService
    {
        private readonly ApplicationDbContext _data;

        public TherapistsServicesService(ApplicationDbContext data)
        {
            _data = data;
        }

        public async Task AddTherapistServiceAsync(IEnumerable<TherapistService> input)
        {
            await _data.TherapistsServices.AddRangeAsync(input);
            await _data.SaveChangesAsync();
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
            await _data.TherapistsServices.AddRangeAsync(therapistServices);
            await _data.SaveChangesAsync();
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
            await _data.TherapistsServices.AddRangeAsync(serviceTherapists);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(string therapistId)
        {
            return await _data.TherapistsServices
                .Include(x => x.Service)
                .Where(x => x.TherapistId.ToString() == therapistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TherapistService>> GetProvidedTherapistServicesByIdAsync(string therapistId)
        {
            return await _data.TherapistsServices
                .Include(x => x.Service)
                .Where(x => x.TherapistId.ToString() == therapistId && x.isProvided)
                .ToListAsync();
        }

        public async Task ChangeProvidedStatusAsync(string therapistId, string serviceId)
        {
            var therapistService = await _data.TherapistsServices
                                .FirstOrDefaultAsync(
                                x => x.ServiceId.ToString() == serviceId
                                && x.TherapistId.ToString() == therapistId);

            therapistService.isProvided = !therapistService.isProvided;

            await _data.SaveChangesAsync();
        }

        public async Task AddTherapistToServiceAsync(string therapistId, string serviceId)
        {
            var therapistService = await _data.TherapistsServices
                                .FirstOrDefaultAsync(
                                x => x.ServiceId.ToString() == serviceId
                                && x.TherapistId.ToString() == therapistId);

            therapistService.isProvided = !therapistService.isProvided;

            await _data.SaveChangesAsync();
        }
    }
}