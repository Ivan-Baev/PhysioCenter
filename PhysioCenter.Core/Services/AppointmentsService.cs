namespace PhysioCenter.Core.Services
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ITherapistsService _therapistsService;
        private readonly IClientsService _clientsService;
        private readonly ITherapistsServicesService _therapistsServicesService;

        public AppointmentsService(IApplicationDbRepository _repo,
            ITherapistsService therapistsService,
            IClientsService clientsService,
            ITherapistsServicesService therapistsServicesService)
        {
            repo = _repo;
            _therapistsService = therapistsService;
            _clientsService = clientsService;
            _therapistsServicesService = therapistsServicesService;
        }

        public async Task<Appointment> GetByIdAsync(Guid id)
        {
            var appointment = await repo.All<Appointment>()
                .Where(x => x.Id == id)
                .Include(c => c.Client)
                .Include(c => c.Therapist)
                .Include(c => c.Service)
               .FirstOrDefaultAsync();

            if (appointment == null)
            {
                throw new ArgumentException("The provided id does not exist");
            }

            return appointment;
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

            return appointments
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();
        }

        public async Task<IEnumerable<Appointment>> GetTodayByTherapistIdAsync(Guid therapistId, DateTime? filterDate)
        {
            await _therapistsService.FindTherapistById(therapistId);

            var appointments = await repo.All<Appointment>()
                .Where(x => x.TherapistId == therapistId)
                 .Include(c => c.Client)
                 .Include(c => c.Service)
                .OrderBy(x => x.DateTime)
                .ToListAsync();

            if (filterDate == null)
            {
                appointments = appointments.Where(c => c.DateTime.Date == DateTime.UtcNow.Date)
                    .ToList();
            }

            if (filterDate != null)
            {
                appointments = appointments.Where(c => c.DateTime.Date == filterDate.Value.Date)
                    .ToList();
            }

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingByTherapistIdAsync(Guid therapistId)
        {
            await _therapistsService.FindTherapistById(therapistId);

            var appointments = await repo.All<Appointment>()
                .Where(x => x.TherapistId == therapistId
                        && x.DateTime >= DateTime.UtcNow)
                 .Include(c => c.Client)
                 .Include(c => c.Service)
                .OrderBy(x => x.DateTime)
                .ToListAsync();

            return appointments;
        }

        public async Task AddAsync(Appointment input)
        {
            await _therapistsService.FindTherapistById(input.TherapistId);
            await _clientsService.FindClientById(input.ClientId);
            await _therapistsServicesService.FindTherapistServiceById(input.TherapistId, input.ServiceId);

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment input)
        {
            await _therapistsService.FindTherapistById(input.TherapistId);
            await _clientsService.FindClientById(input.ClientId);
            await _therapistsServicesService.FindTherapistServiceById(input.TherapistId, input.ServiceId);

            repo.Update(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var appointment = await GetByIdAsync(id);

            repo.Delete(appointment);
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

            return appointments.Count;
        }

        public async Task<JsonResult> GetScheduleList(Guid therapistId)
        {
            var appointments = await GetUpcomingByTherapistIdAsync(therapistId);
            var schedule = new List<string>();
            foreach (var appointment in appointments)
            {
                schedule.Add(appointment.DateTime.ToString("dd/MM/yyyy H"));
            }

            return new JsonResult(schedule);
        }
    }
}