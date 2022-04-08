namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;

    public interface ITherapistsServicesService
    {
        Task<IEnumerable<TherapistService>> GetTherapistServicesByIdAsync(Guid therapistId);

        Task<IEnumerable<TherapistService>> GetProvidedTherapistServicesByIdAsync(Guid therapistId);

        Task AddAllServicesToTherapistId(IEnumerable<Service> services, Guid therapistId);

        Task AddAllTherapistsToServiceId(IEnumerable<Therapist> therapists, Guid serviceId);

        Task FindTherapistServiceById(Guid therapistId, Guid serviceId);

        Task ChangeProvidedStatusAsync(Guid therapistId, Guid serviceId);
    }
}