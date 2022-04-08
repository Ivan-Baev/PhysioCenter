namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsService
    {
        Task<Therapist> GetByIdAsync(Guid id);

        Task<IEnumerable<Therapist>> GetAllAsync();

        Task<Therapist> FindTherapistById(Guid id);

        Task<Therapist> FindTherapistById(string id);

        Task AddAsync(Therapist input);

        Task UpdateDetailsAsync(Therapist input);

        Task DeleteAsync(Guid id);
    }
}