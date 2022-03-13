namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TherapistsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (dbContext.Therapists.Any())
            {
                return;
            }

            var therapist1 = await userManager.FindByEmailAsync("therapist1@physiocenter.com");
            var therapist2 = await userManager.FindByEmailAsync("therapist2@physiocenter.com");
            var therapist3 = await userManager.FindByEmailAsync("therapist3@physiocenter.com");
            var therapist4 = await userManager.FindByEmailAsync("therapist4@physiocenter.com");
            var therapist5 = await userManager.FindByEmailAsync("therapist5@physiocenter.com");
            var therapist6 = await userManager.FindByEmailAsync("therapist6@physiocenter.com");

            var therapists = new Therapist[]

                            {
                                new Therapist
                                {
                                    Id = new Guid("e927b696-781c-4fe3-a748-777df063c469"),
                                    FullName = "George Marasini",
                                    Description = "George Marasini strongly believes that physical therapy can add life to years, not just years to life. He has a passion for helping people and a get sense of satisfaction when his clients acknowledge improvements they’ve made as a result of the treatment. He believes that every injury and dysfunction is unique and every patient responds differently to treatment. This is why he is passionate about providing holistic approach to achieve the best possible outcomes.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist1.Id,
                                },
                                new Therapist
                                {
                                    Id = new Guid("21392157-9603-47fa-88ae-773d4dcaecd3"),
                                    FullName = "Alycia Shavon",
                                    Description = "Alycia Shavon is passionate about helping her clients achieve their goals and get back to activities that are important to them. Her attentive and encouraging approach helps her develop a strong rapport with clients and makes them feel at ease.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist2.Id,
                                },
                                new Therapist
                                {
                                    Id = new Guid("388e1767-5878-444c-9230-d74b6733839a"),
                                    FullName = "Edvin Hester",
                                    Description = "Edvin Hester believes in providing a well-rounded treatment plan to his patients that goes beyond what’s done in the treatment room. He integrates aspects of all of his education, experience as a personal trainer, and skills as a massage therapist into one invaluable rehabilitation service to his clients.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist3.Id,
                                },
                                new Therapist
                                {
                                    Id = new Guid("84d337f1-595d-4a88-a42e-2573081f2e5f"),
                                    FullName = "Polly Linnette",
                                    Description = "Polly Linnette believes that each individual client and injury is unique. She uses her skills in manual therapy and exercise prescription to tailor each therapy session to the individual. She also believes in taking an active approach to therapy and encourages her clients to participate and become active members in their recovery.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist4.Id,
                                },
                                new Therapist
                                {
                                    Id = new Guid("07cf1dca-5e86-4a44-8e22-7b3097d3f139"),
                                    FullName = "Albert Frazier",
                                    Description = "Albert Frazier is focused on developing innovative and individualized programs to rehabilitate people who have sustained a neurological injury, including stroke, brain injury, spinal cord injury, as well as complex orthopedic injuries. At Propel, he pursues his passion for helping people work towards their full potential, no matter their limitations or backgrounds. The capacity for recovery following a neurological injury is his driving force.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist5.Id,
                                },
                                new Therapist
                                {
                                    Id = new Guid("1729b7be-1140-423c-8729-c81b36f4abed"),
                                    FullName = "Martha Lyle",
                                    Description = "Martha Lyle is passionate about helping her clients achieve their health and rehabilitation goals. She believes that by becoming more efficient in our day to day movements, we can significantly improve our quality of life. To achieve this, she helps people to increase overall strength and coordination and to incorporate regular exercise into their lifestyle.",
                                    ProfileImageUrl = "https://cdn2.iconfinder.com/data/icons/random-outline-3/48/random_14-512.png",
                                    UserId = therapist6.Id,
                                },
                            };

            await dbContext.AddRangeAsync(therapists);
        }
    }
}