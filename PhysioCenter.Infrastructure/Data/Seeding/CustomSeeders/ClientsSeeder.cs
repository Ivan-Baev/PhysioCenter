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
                                    Id = new Guid("e927b696-781c-4fe3-a748-777df063c469"),
                                    FirstName = "George",
                                    UserId = user1.Id,
                                },
                                new Client
                                {
                                    Id = new Guid("21392157-9603-47fa-88ae-773d4dcaecd3"),
                                    FirstName = "Peter",
                                    UserId = user2.Id,
                                },
                                new Client
                                {
                                    Id = new Guid("388e1767-5878-444c-9230-d74b6733839a"),
                                    FirstName = "Sylvana",
                                    UserId = user3.Id,
                                },
                            };

            await dbContext.AddRangeAsync(clients);
        }
    }
}