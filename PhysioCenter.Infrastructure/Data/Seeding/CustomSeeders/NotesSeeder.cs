namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByEmailAsync("user@physiocenter.com");
            var therapist = await userManager.FindByEmailAsync("therapist1@physiocenter.com");

            var clientId = dbContext.Clients.FirstOrDefault(x => x.UserId == user.Id).Id;
            var therapistId = dbContext.Therapists.FirstOrDefault(x => x.UserId == therapist.Id).Id;

            if (dbContext.Notes.Any())
            {
                return;
            }

            var notes = new Note[]
                {
                    new Note
                    {
                       Id = Guid.NewGuid(),
                       Content = "Patient feels better after performing 2 sets of Exercise X for 15 repetitions",
                       CreatedOn = DateTime.Now,
                       ClientId = clientId,
                       TherapistId = therapistId,
                    },

                    new Note
                    {
                       Id = Guid.NewGuid(),
                       Content = "Exercise Z made him feel worse. Closing the joints in the lower-right lumbar side causes extreme pain. Avoid!",
                       CreatedOn = DateTime.Now,
                       ClientId = clientId,
                       TherapistId = therapistId,
                    },

                    new Note
                    {
                       Id = Guid.NewGuid(),
                       Content = "Check if exercise wall-side-move is effective at reducing pain for next appointment",
                       CreatedOn = DateTime.Now,
                       ClientId = clientId,
                       TherapistId = therapistId,
                    },
                };

            await dbContext.AddRangeAsync(notes);
        }
    }
}