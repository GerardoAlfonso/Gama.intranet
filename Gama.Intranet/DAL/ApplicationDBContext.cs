﻿using Gama.Intranet.BL.Models;
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
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Folders> Folders { get; set; }
        public DbSet<UsuariosPermisosFolders> UsuariosPermisosFolders { get; set; }
        public DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }
        public DbSet<LogStatus> LogStatuses { get; set; }

        // Admin


        // Routes

        
        // Logs


    }
}
