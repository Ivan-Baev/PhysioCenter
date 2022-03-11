namespace PhysioCenter.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using PhysioCenter.Infrastructure.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Blog> Blog { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Therapist> Therapists { get; set; }

        public DbSet<TherapistService> TherapistsServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
               .Entity<Therapist>()
               .HasOne<IdentityUser>()
               .WithOne()
               .HasForeignKey<Therapist>(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Client>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<TherapistService>()
             .HasKey(x => new { x.ServiceId, x.TherapistId });

            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}