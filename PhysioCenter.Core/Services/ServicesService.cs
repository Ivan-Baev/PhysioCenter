namespace PhysioCenter.Core.Services.Services
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class ServicesService : IServicesService
    {
        private readonly IApplicationDbRepository repo;

        public ServicesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<Service> GetByIdAsync(Guid id)
        {
            var service = await repo.All<Service>()
                .Include(x => x.Category)
                .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (service == null)
            {
                throw new ArgumentException("This service does not exist!");
            }

            return service;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return
                await repo.All<Service>()
                .Include(x => x.Appointments)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Category.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Service input)
        {
            if (repo.All<Service>().Any(c => c.Name == input.Name))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var service = await GetByIdAsync(id);

            repo.Delete<Service>(service);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Service input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }
    }
}