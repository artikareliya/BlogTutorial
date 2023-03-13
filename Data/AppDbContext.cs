using BlogTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogTutorial.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Post> tblPost { get; set; }
        public DbSet<Profile> TblProfaie { get; set; }
    }
}
