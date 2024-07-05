using Microsoft.EntityFrameworkCore;
using SingnalR_Example.Models;

namespace SingnalR_Example.NewFolder
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Item> Items { get; set; }
    }
}
