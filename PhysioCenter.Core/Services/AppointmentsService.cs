namespace PhysioCenter.Core.Services.Appointments
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IApplicationDbRepository repo;

        public AppointmentsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<Appointment> GetByIdAsync(string id)
        {
            return
                await repo.All<Appointment>()
                .Where(x => x.Id == Guid.Parse(id))
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(int page, int itemsPerPage, string? clientName = null)
        {
            var appointments = await repo.All<Appointment>()
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
                .OrderByDescending(x => x.DateTime).ToListAsync();

            if (clientName != null)
            {
                appointments = appointments.Where(c => c.Client.FullName.Contains(clientName, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }

            appointments = appointments
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            if (appointments.Count == 0)
            {
                throw new InvalidOperationException("There are no appointments");
            }

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetTodayByTherapistIdAsync(Guid therapistId)
        {
            return
                await repo.All<Appointment>()
                .Where(x => x.TherapistId == therapistId
                        && x.DateTime.Day == DateTime.UtcNow.Day)
                 .Include(c => c.Client)
                 .Include(c => c.Service)
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(Guid therapistId)
        {
            return
                await repo.All<Appointment>()
                .Where(x => x.TherapistId == therapistId
                        && x.DateTime >= DateTime.UtcNow)
                 .Include(c => c.Client)
                 .Include(c => c.Service)
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task AddAsync(Appointment input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var appointment =
               await repo.All<Appointment>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Appointment>(appointment);
            await repo.SaveChangesAsync();
        }

        public async Task<int> GetCount(string? clientName = null)
        {
            var appointments = await repo.All<Appointment>()
                .Include(c => c.Client)
                .ToListAsync();

            if (clientName != null)
            {
                appointments = appointments.Where(c => c.Client.FullName.Contains(clientName, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }

            return appointments.Count();
        }
    }
}