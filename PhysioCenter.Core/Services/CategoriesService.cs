namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _data;

        public CategoriesService(ApplicationDbContext data)
        {
            _data = data;
        }

        public async Task AddAsync(Category input)
        {
            if (_data.Categories.Any(c => c.Name == input.Name))
                return;

            await _data.Categories.AddAsync(input);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _data.Categories
                .Include(x => x.Services)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return
                await _data.Categories
                .Where(x => x.Id == Guid.Parse(id))
                .Include(x => x.Services)
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Category input)
        {
            _data.Categories.Update(input);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category =
               await _data.Categories
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Categories.Remove(category);
            await _data.SaveChangesAsync();
        }
    }
}