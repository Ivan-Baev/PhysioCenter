﻿namespace PhysioCenter.Core.Contracts
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

        public async Task AddAsync(Category input)
        {
            if (repo.All<Category>().Any(c => c.Name == input.Name))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await repo.All<Category>()
                .Include(x => x.Services)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return
                await repo.All<Category>()
                .Where(x => x.Id == Guid.Parse(id))
                .Include(x => x.Services)
               .FirstOrDefaultAsync();
        }

        public async Task UpdateDetailsAsync(Category input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category =
               await repo.All<Category>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            repo.Delete<Category>(category);
            await repo.SaveChangesAsync();
        }
    }
}