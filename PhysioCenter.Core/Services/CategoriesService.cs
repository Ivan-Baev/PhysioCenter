namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class CategoriesService : ICategoriesService
    {
        private readonly IApplicationDbRepository repo;

        public CategoriesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await repo.All<Category>()
                .Include(x => x.Services)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await repo.All<Category>()
                .Where(x => x.Id == id)
                .Include(x => x.Services)
               .FirstOrDefaultAsync();

            if (category == null)
            {
                throw new ArgumentException("This category doesn't exist!");
            }

            return category;
        }

        public async Task AddAsync(Category input)
        {
            if (repo.All<Category>().Any(c => c.Name == input.Name))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Category input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);

            repo.Delete(category);
            await repo.SaveChangesAsync();
        }
    }
}