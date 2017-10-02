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

        public AppDbContext() : base("Name=WebApplicationDb")
        {
        }

        //db sets
        public DbSet<User> Users { get; set; }
    }
}