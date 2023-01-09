using Gama.Intranet.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Gama.Intranet.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbContext DbContext { set; get; }

        // Admin
        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Folders> Folders { get; set; }
        public DbSet<UsuariosPermisosFolders> UsuariosPermisosFolders { get; set; }
        public DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }
        

        public DbSet<ContenidoHtml> ContenidoHtml { get; set; }

        public DbSet<FoldersCategories> FoldersCategories { get; set; }


        // Catalogue

        // Routes


        // Logs

        public DbSet<Logs> Logs { get; set; }
        public DbSet<LogStatus> LogStatus { get; set; }

    }
}
