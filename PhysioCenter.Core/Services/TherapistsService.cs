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
        private readonly IHtmlSanitizer _htmlSanitizer;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public TherapistsService(IApplicationDbRepository _repo,
            IHtmlSanitizer htmlSanitizer,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            repo = _repo;
            _htmlSanitizer = htmlSanitizer;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
               .FirstOrDefaultAsync();
        }

        public async Task<Therapist> GetByUserIdAsync(string id)
        {
            return
                await repo.All<Therapist>()
                .Where(x => x.UserId == id)
                .Include(x => x.Services).ThenInclude(x => x.Service)
                .Include(x => x.Appointments)
                .Include(x => x.Clients)
                .Include(x => x.Notes)
                .Include(x => x.Reviews)
               .FirstOrDefaultAsync();
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