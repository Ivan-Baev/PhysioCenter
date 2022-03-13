namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
                {
                    new Category
                    {
                        Id = new Guid("4e597ea2-afa0-4916-bb39-3736d62ee22c"),
                        Name = "Massages",
                        Description = "High-quality massage therapy for the treatment of acute and chronic conditions as a result of spinal cord injury, brain injury, stroke, multiple sclerosis, Parkinson’s, and a host of other neurological conditions. We also treat post-surgical, perinatal, arthritic, and stress-related conditions, including knee replacement, back pain, TMJ pain and headaches.",
                        ImageUrl = "https://i.picsum.photos/id/541/536/354.jpg?hmac=xtBzU85Jpd7SAgxFX7pg9CFWR-XLqMiD9Ec1-oUKw0c",
                    },
                    new Category
                    {
                        Id = new Guid("cf72a45c-c195-4ca4-b79a-87392cacf9b5"),
                        Name = "Sports Therapy",
                        Description = "We utilise the principles of sport and exercise science incorporating physiological and pathological processes to prepare the participant for training, competition and where applicable, work..",
                        ImageUrl = "https://i.picsum.photos/id/541/536/354.jpg?hmac=xtBzU85Jpd7SAgxFX7pg9CFWR-XLqMiD9Ec1-oUKw0c",
                    },
                    new Category
                    {
                        Id = new Guid("5f176df0-2547-455c-bd46-b87e58107dc1"),
                        Name = "Physiotherapy",
                        Description = "Our exercise physiologists and kinesiologists have advanced training in developing safe and effective exercise programs based on your current functional level designed to help you reach your goals. They also work hand-in-hand with the rest of our team of sport therapists and registered massage therapists to make sure all bases are covered when starting your exercise program.",
                        ImageUrl = "https://i.picsum.photos/id/541/536/354.jpg?hmac=xtBzU85Jpd7SAgxFX7pg9CFWR-XLqMiD9Ec1-oUKw0c",
                    },
                };

            // Need them in particular order
            foreach (var category in categories)
            {
                await dbContext.AddAsync(category);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}