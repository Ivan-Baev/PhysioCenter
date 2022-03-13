namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByEmailAsync("user@physiocenter.com");
            var therapist = await userManager.FindByEmailAsync("therapist1@physiocenter.com");

            var clientId = dbContext.Clients.FirstOrDefault(x => x.UserId == user.Id).Id;
            var therapistId = dbContext.Therapists.FirstOrDefault(x => x.UserId == therapist.Id).Id;

            if (dbContext.Reviews.Any())
            {
                return;
            }

            var reviews = new Review[]
                {
                    new Review
                    {
                        Id = Guid.NewGuid(),
                        Content = "I had a traumatic brain injury and my progress has been outstanding thanks to the knowledge and encouragement of the staff in the clinic. Everyone is so friendly and welcoming, you can really tell they take pride in their work. I would definitely recommend this location because they produce results!",
                        CreatedOn = DateTime.UtcNow,
                        ClientId = clientId,
                        TherapistId = therapistId,
                    },
                };

            await dbContext.AddRangeAsync(reviews);
        }
    }
}