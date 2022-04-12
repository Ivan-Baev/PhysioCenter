namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (dbContext.Appointments.Any())
            {
                return;
            }

            var appointments = new List<Appointment>();

            var user = await userManager.FindByEmailAsync("user@physiocenter.com");
            var clientId = dbContext.Clients.FirstOrDefault(x => x.UserId == user.Id).Id;
            // Get Therapists Ids
            var therapistsIds = await dbContext.Therapists.Select(x => x.Id).Take(6).ToListAsync();

            foreach (var therapistId in therapistsIds)
            {
                // Get a Service from each Therapist
                var service = await dbContext.TherapistsServices.FirstOrDefaultAsync((x => x.TherapistId == therapistId));

                // Add Upcoming Appointments
                appointments.Add(new Appointment
                {
                    Id = Guid.NewGuid(),
                    DateTime = new DateTime(2022, 04, 20, 10, 0, 0),
                    ClientId = clientId,
                    TherapistId = therapistId,
                    ServiceId = service.ServiceId,
                });

                appointments.Add(new Appointment
                {
                    Id = Guid.NewGuid(),
                    DateTime = new DateTime(2022, 04, 18, 10, 0, 0),
                    ClientId = clientId,
                    TherapistId = therapistId,
                    ServiceId = service.ServiceId,
                });

                appointments.Add(new Appointment
                {
                    Id = Guid.NewGuid(),
                    DateTime = new DateTime(2022, 04, 17, 10, 0, 0),
                    ClientId = clientId,
                    TherapistId = therapistId,
                    ServiceId = service.ServiceId,
                });
            }

            await dbContext.AddRangeAsync(appointments);
        }
    }
}