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
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var therapistsClients = new List<TherapistClient>();

            var user = await userManager.FindByEmailAsync("user@physiocenter.com");
            var therapist = await userManager.FindByEmailAsync("therapist1@physiocenter.com");

            var clientId = dbContext.Clients.FirstOrDefault(x => x.UserId == user.Id).Id;
            var therapistId = dbContext.Therapists.FirstOrDefault(x => x.UserId == therapist.Id).Id;

            therapistsClients.Add(new TherapistClient
            {
                TherapistId = therapistId,
                ClientId = clientId,
            });

            await dbContext.AddRangeAsync(therapistsClients);
        }
    }
}