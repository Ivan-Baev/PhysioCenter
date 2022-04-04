namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class ReviewsService : IReviewsService
    {
        private readonly IApplicationDbRepository repo;

        public ReviewsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await repo.All<Review>()
                .Include(x => x.Client)
                .Include(x => x.Therapist)
                .ToListAsync();
        }

        public async Task AddAsync(Review input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var review =
               await repo.All<Review>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Review>(review);
            await repo.SaveChangesAsync();
        }
    }
}