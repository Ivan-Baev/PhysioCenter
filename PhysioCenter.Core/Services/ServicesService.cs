namespace PhysioCenter.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class ServicesService : IServicesService
    {
        private readonly IApplicationDbRepository repo;
        private readonly ICategoriesService _categoriesService;

        public ServicesService(
            IApplicationDbRepository _repo,
            ICategoriesService categoriesService)
        {
            repo = _repo;
            _categoriesService = categoriesService;
        }

        public async Task<Service> GetByIdAsync(Guid id)
        {
            var service = await repo.All<Service>()
                .Include(x => x.Category)
                .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (service == null)
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.InvalidServiceId);
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
            GuardAgainstSameName(input);

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Service input)
        {
            await _categoriesService.GetByIdAsync(input.CategoryId);
            var service = await GetByIdAsync(input.Id);

            if (service.Name != input.Name)
            {
                GuardAgainstSameName(input);
            }

            service.CategoryId = input.CategoryId;
            service.Description = input.Description;
            service.Price = input.Price;
            service.Name = input.Name;
            service.Price = input.Price;

            repo.Update(service);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var service = await GetByIdAsync(id);

            repo.Delete(service);
            await repo.SaveChangesAsync();
        }

        private void GuardAgainstSameName(Service input)
        {
            if (repo.All<Service>().Any(c => c.Name == input.Name))
            {
                throw new ArgumentException(Utilities.Constants.ErrorMessages.DuplicateServiceName);
            }
        }
    }
}