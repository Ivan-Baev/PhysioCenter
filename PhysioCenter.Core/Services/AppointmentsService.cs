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

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await repo.All<Appointment>()
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
                .OrderByDescending(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllByTherapistIdAsync(string therapistId)
        {
            return
                 await repo.All<Appointment>()
                 .Where(x => x.TherapistId == Guid.Parse(therapistId))
                 .Include(c => c.Client)
                 .OrderByDescending(x => x.DateTime)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(string therapistId)
        {
            return
                await repo.All<Appointment>()
                .Where(x => x.TherapistId == Guid.Parse(therapistId)
                        && x.DateTime > DateTime.UtcNow)
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetPastByUserAsync(string userId)
        {
            return
                     await repo.All<Appointment>()
                     .Where(x => x.ClientId == Guid.Parse(userId)
                             && x.DateTime.Date < DateTime.UtcNow.Date)
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
    }
}