namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsService
    {
        Task<Therapist> GetByIdAsync(string id);

        Task<IEnumerable<Therapist>> GetAllAsync();

        Guid FindTherapistId(string id);

        Task AddAsync(Therapist input);

        Task UpdateDetailsAsync(Therapist input);

        Task DeleteAsync(string id);
    }
}