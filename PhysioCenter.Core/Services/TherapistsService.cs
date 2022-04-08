namespace PhysioCenter.Core.Services.Therapists
{
    using Ganss.XSS;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Repository;

    public class TherapistsService : ITherapistsService
    {
        private readonly IApplicationDbRepository repo;
        private readonly UserManager<IdentityUser> userManager;

        public TherapistsService(IApplicationDbRepository _repo,
            UserManager<IdentityUser> userManager)
        {
            repo = _repo;
            this.userManager = userManager;
        }

        public async Task<Therapist> GetByIdAsync(string id)
        {
            return
                await repo.All<Therapist>()
                .Where(x => x.Id == Guid.Parse(id))
                .Include(x => x.Services).ThenInclude(x => x.Service)
                .Include(x => x.Appointments)
                .Include(x => x.Clients)
                .Include(x => x.Notes)
                .Include(x => x.Reviews)
                .AsSplitQuery()
               .FirstOrDefaultAsync();
        }

        public Guid FindTherapistId(string id)
        {
            var therapist = repo.All<Therapist>()
               .FirstOrDefault(x => x.UserId == id);

            if (therapist == null)
            {
                throw new ArgumentNullException(nameof(id), "This therapist does not exist!");
            }

            return therapist.Id;
        }

        public async Task<IEnumerable<Therapist>> GetAllAsync()
        {
            return
                await repo.All<Therapist>()
                .OrderByDescending(x => x.FullName)
                .ToListAsync();
        }

        public async Task AddAsync(Therapist input)
        {
            if (repo.All<Therapist>().Any(c => c.FullName == input.FullName))
                return;

            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Therapist input)
        {
            repo.Update(input);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var therapist =
               await repo.All<Therapist>()
               .Include(x => x.Services)
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            var userToDelete = await userManager.FindByIdAsync(therapist.UserId);

            repo.Delete<Therapist>(therapist);
            await userManager.DeleteAsync(userToDelete);
            await repo.SaveChangesAsync();
        }
    }
};