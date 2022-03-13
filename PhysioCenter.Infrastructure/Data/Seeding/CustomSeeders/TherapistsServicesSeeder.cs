namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TherapistsServices : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TherapistsServices.Any())
            {
                return;
            }

            var therapistServices = new List<TherapistService>();

            foreach (var therapist in dbContext.Therapists)
            {
                var therapistId = therapist.Id;

                foreach (var service in dbContext.Services)
                {
                    var serviceId = service.Id;

                    therapistServices.Add(new TherapistService
                    {
                        TherapistId = therapistId,
                        ServiceId = serviceId,
                        isProvided = true,
                    });
                }
            }

            await dbContext.AddRangeAsync(therapistServices);
        }
    }
}