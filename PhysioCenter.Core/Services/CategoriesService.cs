namespace PhysioCenter.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class CategoriesService :  ICategoriesService
    {
        private readonly ApplicationDbContext _data;

        public CategoriesService(ApplicationDbContext data)
        {
            _data = data;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _data.Categories
                .Include(x => x.Services)
                .ToListAsync();
        }
    }
}
