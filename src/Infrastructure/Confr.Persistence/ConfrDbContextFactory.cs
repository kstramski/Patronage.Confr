using Microsoft.EntityFrameworkCore;
using Confr.Persistence.Infrastructure;

namespace Confr.Persistence
{
    public class ConfrDbContextFactory : DesignTimeDbContextFactoryBase<ConfrDbContext>
    {
        protected override ConfrDbContext CreateNewInstance(DbContextOptions<ConfrDbContext> options)
        {
            return new ConfrDbContext(options);
        }
    }
}
