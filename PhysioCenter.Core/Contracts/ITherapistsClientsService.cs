namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;

    public interface ITherapistsClientsService
    {
        Task<IEnumerable<TherapistClient>> GetProvidedClientTherapistsByIdAsync(Guid clientId);

        Task<IEnumerable<TherapistClient>> GetProvidedTherapistClientsByIdAsync(Guid therapistId);

        Task AddTherapistClientAsync(TherapistClient input);
    }
}