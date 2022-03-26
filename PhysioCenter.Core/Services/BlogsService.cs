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

        public async Task AddAsync(Blog input)
        {
            if (_data.Blog.Any(c => c.Title == input.Title))
                return;

            await _data.Blog.AddAsync(input);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var blog =
               await _data.Blog
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Blog.Remove(blog);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _data.Blog
                .ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(string id)
        {
            return
                await _data.Blog
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Blog input)
        {
            _data.Blog.Update(input);
            await _data.SaveChangesAsync();
        }
    }
}