namespace PhysioCenter.Core.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
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

        public async Task<Therapist> GetByIdAsync(Guid id)
        {
            var therapist = await repo.All<Therapist>()
                .Where(x => x.Id == id)
                .Include(x => x.Services).ThenInclude(x => x.Service)
                .Include(x => x.Appointments)
                .Include(x => x.Clients)
                .Include(x => x.Notes)
                .Include(x => x.Reviews)
                .AsSplitQuery()
               .FirstOrDefaultAsync();

            if (therapist == null)
            {
                throw new ArgumentException("This therapist does not exist!");
            }

            return therapist;
        }

        public async Task<Therapist> FindTherapistById(string userId)
        {
            var therapist = await repo.All<Therapist>()
               .FirstOrDefaultAsync(x => x.UserId == userId);

            if (therapist == null)
            {
                throw new ArgumentException("This therapist does not exist!");
            }

            return therapist;
        }

        public async Task<Therapist> FindTherapistById(Guid id)
        {
            var therapist = await repo.All<Therapist>()
               .FirstOrDefaultAsync(x => x.Id == id)
               ;

            if (therapist == null)
            {
                throw new ArgumentException("This therapist does not exist!");
            }

            return therapist;
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
            await repo.AddAsync(input);
            await repo.SaveChangesAsync();
        }

        public async Task UpdateDetailsAsync(Therapist input)
        {
            var therapist = await FindTherapistById(input.Id);
            therapist.Description = input.Description;
            therapist.ProfileImageUrl = input.ProfileImageUrl;

            repo.Update(therapist);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var therapist = await FindTherapistById(id);

            var userToDelete = await userManager.FindByIdAsync(therapist.UserId);

            repo.Delete(therapist);
            await userManager.DeleteAsync(userToDelete);
            await repo.SaveChangesAsync();
        }
    }
};