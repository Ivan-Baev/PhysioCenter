namespace PhysioCenter.Core.Services
{
    using PhysioCenter.Core.Contracts;
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
                throw new ArgumentException(Utilities.Constants.ErrorMessages.InvalidBlogId);
            }

            return blog;
        }

        public async Task AddAsync(Blog input)
        {
            GuardAgainstSameTitle(input);

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Blog input)
        {
            var blog = await GetByIdAsync(input.Id);

            if (blog.Title != input.Title)
            {
                GuardAgainstSameTitle(input);
            }

            blog.Title = input.Title;
            blog.Content = input.Content;
            blog.ImageUrl = input.ImageUrl;

            repo.Update(blog);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var blog = await GetByIdAsync(id);

            repo.Delete(blog);
            await repo.SaveChangesAsync();
        }

        private void GuardAgainstSameTitle(Blog input)
        {
            if (repo.All<Blog>().Any(c => c.Title == input.Title))
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.DuplicateBlogTitle);
            }
        }
    }
}