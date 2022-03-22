namespace PhysioCenter.Models.Categories
{
    using PhysioCenter.Infrastructure.Data.Models;

    using System;

    public class CategoryViewModel
    {
        public Guid Id;

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}