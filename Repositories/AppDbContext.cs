using System.Data.Entity;
using Models.Entities;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext()
        {
        }

        //db sets
        public DbSet<User> Users { get; set; }
    }
}