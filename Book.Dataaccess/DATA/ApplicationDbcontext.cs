
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.Dataaccess
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

    }
}
