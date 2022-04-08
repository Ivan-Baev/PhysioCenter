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

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await repo.All<Blog>()
                .ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(Guid id)
        {
            var blog = await repo.All<Blog>()
                .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (blog == null)
            {
                throw new ArgumentException("This blog does not exist");
            }

            return blog;
        }

        public async Task AddAsync(Blog input)
        {
            if (repo.All<Blog>().Any(c => c.Title == input.Title))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Blog input)
        {
            var blog = await GetByIdAsync(input.Id);

            repo.Update(blog);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var blog = await GetByIdAsync(id);

            repo.Delete(blog);
            await repo.SaveChangesAsync();
        }
    }
}