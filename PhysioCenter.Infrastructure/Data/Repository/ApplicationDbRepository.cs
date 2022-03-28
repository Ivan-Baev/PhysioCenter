namespace PhysioCenter.Infrastructure.Data.Repository
{
    using PhysioCenter.Infrastructure.Data.Common;

    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}