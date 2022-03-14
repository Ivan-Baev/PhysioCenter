namespace PhysioCenter.Core.Services.Appointments
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Core.Mappings;
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

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var appointment =
                await _data.Appointments
                .Where(x => x.Id == Guid.Parse(id))
                .To<T>()
               .FirstOrDefaultAsync();
            return appointment;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var appointments =
                await _data.Appointments
                .OrderByDescending(x => x.DateTime)
                .To<T>().ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<T>> GetAllByTherapistAsync<T>(string therapistId)
        {
            var appointments =
                await _data.Appointments
                .Where(x => x.TherapistId == Guid.Parse(therapistId))
                .OrderByDescending(x => x.DateTime)
                .To<T>().ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<T>> GetUpcomingByUserAsync<T>(string userId)
        {
            var appointments =
                await _data.Appointments
                .Where(x => x.ClientId == Guid.Parse(userId)
                        && x.DateTime.Date > DateTime.UtcNow.Date)
                .OrderBy(x => x.DateTime)
                .To<T>().ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<T>> GetPastByUserAsync<T>(string userId)
        {
            var appointments =
                await _data.Appointments
                .Where(x => x.ClientId == Guid.Parse(userId)
                        && x.DateTime.Date < DateTime.UtcNow.Date)
                .OrderBy(x => x.DateTime)
                .To<T>().ToListAsync();
            return appointments;
        }

        public async Task AddAsync(string userId, string salonId, string serviceId, DateTime dateTime)
        {
            await _data.Appointments.AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                DateTime = dateTime,
                ClientId = Guid.Parse(userId),
                TherapistId = Guid.Parse(salonId),
                ServiceId = Guid.Parse(serviceId),
            });
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