namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    public interface ITherapistsService
    {
        Task<Therapist> GetByIdAsync(string id);

        Task<IEnumerable<Therapist>> GetAllAsync();

        Task AddAsync(Therapist input);

        Task DeleteAsync(string id);
    }
}