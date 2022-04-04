namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;

    public interface ITherapistsClientsService
    {
        Task<IEnumerable<TherapistClient>> GetProvidedClientTherapistsByIdAsync(string clientId);

        Task<IEnumerable<TherapistClient>> GetProvidedTherapistClientsByIdAsync(string therapistId);

        Task AddTherapistClientAsync(TherapistClient input);
    }
}