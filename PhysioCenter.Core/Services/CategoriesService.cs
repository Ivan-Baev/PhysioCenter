namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
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
            GuardAgainstSameTitle(input);

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Category input)
        {
            var category = await GetByIdAsync(input.Id);

            if (category.Name != input.Name)
            {
                GuardAgainstSameTitle(input);
            }

            category.Description = input.Description;
            category.Name = input.Name;
            category.ImageUrl = input.ImageUrl;

            repo.Update(category);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);

            repo.Delete(category);
            await repo.SaveChangesAsync();
        }

        private void GuardAgainstSameTitle(Category input)
        {
            if (repo.All<Category>().Any(c => c.Name == input.Name))
            {
                throw new ArgumentException("This category name already exists");
            }
        }
    }
}