namespace PhysioCenter.Core.Contracts
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppointmentsService
    {
        Task<Appointment> GetByIdAsync(Guid id);

        Task<IEnumerable<Appointment>> GetAllAsync(int page, int itemsPerPage, string? clientName = null);

        Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(Guid therapistId);

        Task<IEnumerable<Appointment>> GetTodayByTherapistIdAsync(Guid therapistId, DateTime? filterDate);

        Task AddAsync(Appointment input);

        Task UpdateAsync(Appointment input);

        Task DeleteAsync(Guid id);

        Task<int> GetCount(string? clientName = null);
    }
}