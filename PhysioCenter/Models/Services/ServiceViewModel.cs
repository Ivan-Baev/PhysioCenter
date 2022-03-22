namespace PhysioCenter.Models.Services
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System;

    public class ServiceViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}