namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class ReviewsService:  IReviewsService
    {
        private readonly ApplicationDbContext _data;

        public ReviewsService(ApplicationDbContext data)
        {
            _data = data;
        }
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _data.Reviews
                .Include(x => x.Client)
                .Include(x => x.Therapist)
                .ToListAsync();
        }
    }
}
