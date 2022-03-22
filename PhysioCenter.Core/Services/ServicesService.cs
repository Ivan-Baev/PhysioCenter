﻿namespace PhysioCenter.Core.Services.Services
{
    using Ganss.XSS;

    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Core.Contracts;
    using PhysioCenter.Infrastructure.Data;
    using PhysioCenter.Infrastructure.Data.Models;

    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _data;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public ServicesService(ApplicationDbContext data, IHtmlSanitizer htmlSanitizer)
        {
            _data = data;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<Service> GetByIdAsync(string id)
        {
            return
                await _data.Services
                .Where(x => x.Id == Guid.Parse(id))
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return
                await _data.Services
                .Include(x => x.Appointments)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Category.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Service input)
        {
            if (_data.Services.Any(c => c.Name == input.Name))
                return;

            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Description = _htmlSanitizer.Sanitize(input.Description),
                ImageUrl = _htmlSanitizer.Sanitize(input.ImageUrl),
                Name = _htmlSanitizer.Sanitize(input.Name),
                Price = input.Price,
                CategoryId = input.Category.Id,
            };

            await _data.Services.AddAsync(service);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var service =
               await _data.Services
                .Where(x => x.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            _data.Services.Remove(service);
            await _data.SaveChangesAsync();
        }
    }
}