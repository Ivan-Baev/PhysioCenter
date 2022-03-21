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

        public Task ChangeIsProvidedAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TherapistService> GetTherapistServiceById(string therapistId, string serviceId)
        {
           return await _data.TherapistsServices
                                .FirstOrDefaultAsync(
                                x => x.ServiceId.ToString() == serviceId
                                && x.TherapistId.ToString() == therapistId);
        }
    }
}
