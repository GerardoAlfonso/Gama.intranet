using Gama.Intranet.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Gama.Intranet.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbContext DbContext { set; get; }


        // Catalogue
        public DbSet<Usuario> Usuario { get; set; }

        // Admin


        // Routes

        
        // Logs


    }
}
