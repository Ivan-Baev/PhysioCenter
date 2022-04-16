namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
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

        public async Task<Review> GetByIdAsync(Guid id)
        {
            var review = await repo.All<Review>()
                  .Where(x => x.Id == id)
                  .Include(x => x.Client)
                 .FirstOrDefaultAsync();

            if (review == null)
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.InvalidReviewId);
            }
            return review;
        }

        public async Task AddAsync(Review input)
        {
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await GetByIdAsync(id);

            repo.Delete(review);
            await repo.SaveChangesAsync();
        }
    }
}