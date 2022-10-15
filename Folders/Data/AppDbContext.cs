namespace Folders.Data
{
    using Folders.Data.DbModels;
    using Microsoft.EntityFrameworkCore;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Folder> Folders { get; set; }
    }
}
