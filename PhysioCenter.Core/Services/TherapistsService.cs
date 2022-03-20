namespace PhysioCenter.Core.Services.Therapists
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class TherapistsService : ITherapistsService
    {
        private readonly ApplicationDbContext _data;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public TherapistsService(ApplicationDbContext data, IHtmlSanitizer htmlSanitizer)
        {
            _data = data;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<Therapist> GetByIdAsync(string id)
        {
            return
                await _data.Therapists
                .Include(x => x.Services)
                .Include(x => x.Appointments)
                .Include(x => x.Clients)
                .Include(x => x.Notes)
                .Include(x => x.Reviews)
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Therapist>> GetAllAsync()
        {
            return
                await _data.Therapists
                .OrderByDescending(x => x.FullName)
                .Include(x => x.Services)
                .Include(x => x.Appointments)
                .Include(x => x.Clients)
                .Include(x => x.Notes)
                .Include(x => x.Reviews)
                .ToListAsync();
        }

        public async Task AddAsync(Therapist input)
        {
            if (_data.Therapists.Any(c => c.FullName == input.FullName))
                return;

            await _data.Therapists.AddAsync(new Therapist
            {
                Id = Guid.NewGuid(),
                FullName = _htmlSanitizer.Sanitize(input.FullName),
                Description = _htmlSanitizer.Sanitize(input.Description),
                ProfileImageUrl = input.ProfileImageUrl,
            });
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var therapist =
               await _data.Therapists
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Therapists.Remove(therapist);
            await _data.SaveChangesAsync();
        }
    }
}