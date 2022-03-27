namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsService
    {
        Task<Therapist> GetByIdAsync(string id);

        Task<Therapist> GetByUserIdAsync(string id);

        Task<IEnumerable<Therapist>> GetAllAsync();

        Task AddAsync(Therapist input);

        Task UpdateDetailsAsync(Therapist input);

        Task DeleteAsync(string id);
    }
}