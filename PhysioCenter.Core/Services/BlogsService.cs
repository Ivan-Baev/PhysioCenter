namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class BlogsService : IBlogsService
    {

        private readonly ApplicationDbContext _data;

        public BlogsService(ApplicationDbContext data)
        {
            _data = data;
        }
        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _data.Blog
                .ToListAsync();
        }
    }
}
