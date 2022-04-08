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
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IApplicationDbRepository repo;

        public ServicesService(IApplicationDbRepository _repo, IHtmlSanitizer _htmlSanitizer)
        {
            repo = _repo;
            htmlSanitizer = _htmlSanitizer;
        }

        public async Task<Service> GetByIdAsync(string id)
        {
            return
                await repo.All<Service>()
                .Include(x => x.Category)
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
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

        public async Task DeleteAsync(string id)
        {
            var service =
               await repo.All<Service>()
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

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