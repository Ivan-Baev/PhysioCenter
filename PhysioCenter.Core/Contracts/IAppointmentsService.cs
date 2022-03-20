namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        Task<Appointment> GetByIdAsync(string id);

        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetAllByTherapistIdAsync(string therapistId);

        Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(string therapistId);

        Task<IEnumerable<Appointment>> GetPastByUserAsync(string userId);

        Task AddAsync(Appointment input);

        Task UpdateAsync(Appointment input);

        Task DeleteAsync(string id);
    }
}