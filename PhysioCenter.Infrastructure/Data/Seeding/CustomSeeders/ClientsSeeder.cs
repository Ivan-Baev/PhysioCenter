namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ClientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (dbContext.Clients.Any())
            {
                return;
            }

            var user1 = await userManager.FindByEmailAsync("user@physiocenter.com");
            var user2 = await userManager.FindByEmailAsync("user2@physiocenter.com");
            var user3 = await userManager.FindByEmailAsync("user3@physiocenter.com");

            var clients = new Client[]

                            {
                                new Client
                                {
                                    Id = new Guid("ec2aeec7-2f91-406c-927d-366eedccdf1b"),
                                    FullName = "George Hutchinson",
                                    UserId = user1.Id,
                                },
                                new Client
                                {
                                    Id = new Guid("c159d88b-3547-489a-83b7-8a1a011755f9"),
                                    FullName = "Leonardo Callaghan",
                                    UserId = user2.Id,
                                },
                                new Client
                                {
                                    Id = new Guid("4f33a224-57b2-4cdc-8746-af998e09ab9d"),
                                    FullName = "Alivia Brook",
                                    UserId = user3.Id,
                                },
                            };

            await dbContext.AddRangeAsync(clients);
        }
    }
}