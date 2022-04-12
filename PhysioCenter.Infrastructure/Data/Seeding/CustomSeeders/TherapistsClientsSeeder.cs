namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TherapistsClients : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TherapistsClients.Any())
            {
                return;
            }

            var therapistClients = new List<TherapistClient>();

            foreach (var therapist in dbContext.Therapists)
            {
                var therapistId = therapist.Id;

                foreach (var client in dbContext.Clients.Where(x => x.Id == Guid.Parse("EC2AEEC7-2F91-406C-927D-366EEDCCDF1B")))
                {
                    var clientId = client.Id;

                    therapistClients.Add(new TherapistClient
                    {
                        TherapistId = therapistId,
                        ClientId = clientId,
                    });
                }
            }

            await dbContext.AddRangeAsync(therapistClients);
        }
    }
}