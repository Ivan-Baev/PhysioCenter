namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;

    public interface ITherapistsServicesService
    {
        Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(string therapistId);

        Task<IEnumerable<TherapistService>> GetProvidedTherapistServicesByIdAsync(string therapistId);

        Task AddTherapistServiceAsync(IEnumerable<TherapistService> input);

        Task AddAllServicesToTherapistId(IEnumerable<Service> services, Guid therapistId);

        Task AddAllTherapistsToServiceId(IEnumerable<Therapist> therapists, Guid serviceId);

        Task ChangeProvidedStatusAsync(string therapistId, string serviceId);
    }
}