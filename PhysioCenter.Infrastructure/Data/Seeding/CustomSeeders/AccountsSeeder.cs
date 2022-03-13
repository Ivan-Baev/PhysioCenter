namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create Admin
            await CreateUser(
                userManager,
                roleManager,
               "admin@physiocenter.com",
                "Administrator");

            // Create Therapist
            await CreateUser(
                userManager,
                roleManager,
                "therapist1@physiocenter.com",
                "Therapist");

            await CreateUser(
                userManager,
                roleManager,
                "therapist2@physiocenter.com",
                "Therapist");

            await CreateUser(
                userManager,
                roleManager,
                "therapist3@physiocenter.com",
                "Therapist");

            await CreateUser(
                userManager,
                roleManager,
                "therapist4@physiocenter.com",
                "Therapist");

            await CreateUser(
                userManager,
                roleManager,
                "therapist5@physiocenter.com",
                "Therapist");

            await CreateUser(
                userManager,
                roleManager,
                "therapist6@physiocenter.com",
                "Therapist");

            // Create User
            await CreateUser(
                userManager,
                roleManager,
                "user@physiocenter.com",
                "Standard User");

            await CreateUser(
                userManager,
                roleManager,
                "user2@physiocenter.com",
                "Standard User");

            await CreateUser(
                userManager,
                roleManager,
                "user3@physiocenter.com",
                "Standard User");
        }

        private static async Task CreateUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            string email,
            string? roleName = null)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
            };

            var password = "asdasd";
            var usersAssignedToRole = await userManager.GetUsersInRoleAsync(roleName);

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (usersAssignedToRole.Count == 0)
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
                else
                {
                    if (usersAssignedToRole.Count() != 0)
                    {
                        var result = await userManager.CreateAsync(user, password);
                    }
                }
            }
        }
    }
}