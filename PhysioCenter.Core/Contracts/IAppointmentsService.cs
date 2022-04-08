namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        Task<Appointment> GetByIdAsync(string id);

        Task<IEnumerable<Appointment>> GetAllAsync(int page, int itemsPerPage, string? clientName = null);

        Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(Guid therapistId);

        Task<IEnumerable<Appointment>> GetTodayByTherapistIdAsync(Guid therapistId);

        Task AddAsync(Appointment input);

        Task UpdateAsync(Appointment input);

        Task DeleteAsync(string id);

        Task<int> GetCount(string? clientName = null);
    }
}