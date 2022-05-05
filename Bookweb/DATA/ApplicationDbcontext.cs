using Bookweb.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookweb.DATA
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

    }
}
