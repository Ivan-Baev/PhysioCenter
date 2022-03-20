namespace PhysioCenter.Core.Services.Appointments
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly ApplicationDbContext _data;

        public AppointmentsService(ApplicationDbContext data)
        {
            _data = data;
        }

        public async Task<Appointment> GetByIdAsync(string id)
        {
            return
                await _data.Appointments
                .Where(x => x.Id == Guid.Parse(id))
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _data.Appointments
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
                .OrderByDescending(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllByTherapistIdAsync(string therapistId)
        {
            return
                 await _data.Appointments
                 .Where(x => x.TherapistId == Guid.Parse(therapistId))
                 .OrderByDescending(x => x.DateTime)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(string therapistId)
        {
            return
                await _data.Appointments
                .Where(x => x.TherapistId == Guid.Parse(therapistId)
                        && x.DateTime > DateTime.UtcNow)
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetPastByUserAsync(string userId)
        {
            return
                     await _data.Appointments
                     .Where(x => x.ClientId == Guid.Parse(userId)
                             && x.DateTime.Date < DateTime.UtcNow.Date)
                     .OrderBy(x => x.DateTime)
                    .ToListAsync();
        }

        public async Task AddAsync(Appointment input)
        {
            await _data.Appointments.AddAsync(input);
            await _data.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment input)
        {
            _data.Appointments.Update(input);
            await _data.SaveChangesAsync();
        }


        public async Task DeleteAsync(string id)
        {
            var appointment =
               await _data.Appointments
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Appointments.Remove(appointment);
            await _data.SaveChangesAsync();
        }
    }
}