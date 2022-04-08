namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class BlogsService : IBlogsService
    {
        private readonly IApplicationDbRepository repo;

        public BlogsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAsync(Blog input)
        {
            if (repo.All<Blog>().Any(c => c.Title == input.Title))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var blog =
               await repo.All<Blog>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Blog>(blog);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await repo.All<Blog>()
                .ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(string id)
        {
            return
                await repo.All<Blog>()
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Blog input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }
    }
}