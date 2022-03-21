namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsServicesService
    {
        Task<TherapistService> GetTherapistServiceById(string therapistId, string serviceId);

        Task AddTherapistServiceAsync(IEnumerable<TherapistService> input);

        Task ChangeIsProvidedAsync(string id);
    }
}
