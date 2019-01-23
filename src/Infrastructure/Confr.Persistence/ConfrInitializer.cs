using System.Linq;

namespace Confr.Persistence
{
    public class ConfrInitializer
    {
        public static void Initialize(ConfrDbContext context)
        {
            var initializer = new ConfrInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(ConfrDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Rooms.Any())
            {
                return; // Db has been seeded
            }
        }
    }
}