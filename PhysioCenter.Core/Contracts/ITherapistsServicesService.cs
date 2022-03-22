namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsServicesService
    {
        Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(string therapistId);

        Task AddTherapistServiceAsync(IEnumerable<TherapistService> input);

        Task AddAllServicesToTherapistId(IEnumerable<Service> services, Guid therapistId);

        Task ChangeProvidedStatusAsync(string therapistId, string serviceId);
    }
}
