using EFCore.BulkExtensions;
using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
using Gama.Intranet.Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gama.Intranet.BL.Implements
{
    public class AdminImplement : AdminDAO
    {
        private readonly ApplicationDBContext context;

        public AdminImplement(ApplicationDBContext context)
        {
            this.context = context;
        }

        public int deleteById(Usuario entity)
        {
            context.Usuario.Remove(entity);
            context.SaveChanges();
            return 1;
        }

        public List<Usuario> getAll()
        {
            return context.Usuario.ToList();
        }

        public Usuario getById(long id)
        {
            return context.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public int insert(Usuario entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.ShouldChangePassword = false;
            entity.Status = 1;
            entity.Password = Crypto.GetSHA256(entity.Password);
            context.Usuario.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public string ResetPasswordUser(int Id)
        {
            Usuario entity = context.Usuario.FirstOrDefault(x => x.Id == Id);
            string password = Crypto.RandomString(8);
            entity.Password = Crypto.GetSHA256(password);
            entity.UpdatedAt = DateTime.Now;
            entity.ShouldChangePassword = true;
            context.SaveChanges();
            return password;
        }

        public List<Usuario> GetUsers()
        {
            //return context.Usuario.Where(x => x.Status == 1).ToList();
            return context.Usuario.ToList();
        }
        public List<Usuario> GetAllUsers()
        {
            return context.Usuario.ToList();
        }

        public List<Usuario> GetAllUsersActive()
        {
            return context.Usuario.Where(x => x.Status == 1).ToList();
        }

        public int update(Usuario DBEntity, Usuario entity)
        {
            DBEntity.Password = DBEntity.Password == entity.Password ? DBEntity.Password : Crypto.GetSHA256(entity.Password);
            DBEntity.UpdatedAt = DateTime.Now;
            DBEntity.Name = entity.Name;
            DBEntity.Role = entity.Role;
            DBEntity.Status = entity.Status;
            context.SaveChanges();
            return entity.Id;
        }



        // ROLES
        public List<Roles> GetRoles()
        {
            return context.Roles.ToList();
        }

        // STATUS
        public List<LogStatus> GetStatus()
        {
            return context.LogStatus.ToList();
        }


        // Permissions
        public List<UsuariosPermisosFolders> GetPermissionsFolders(int IdUser)
        {
            return context.UsuariosPermisosFolders.Where(x => x.IdUsuario == IdUser).ToList();
        }

        public void UpdatePermissionsUser(List<UpdatePermissionsDAO> obj)
        {
            var results = context.UsuariosPermisosFolders.Where(x => x.IdUsuario == obj[0].IdUser).ToList();
            List<UsuariosPermisosFolders> permisos = new List<UsuariosPermisosFolders>();
            foreach(var item in results)
            {
                context.UsuariosPermisosFolders.Remove(item);
            }
            context.SaveChanges();

            foreach (var item in obj)
            {
                UsuariosPermisosFolders objPermisos = new UsuariosPermisosFolders();
                objPermisos.IdUsuario = item.IdUser;
                objPermisos.Write = item.escritura;
                objPermisos.Read = item.escritura;
                permisos.Add(objPermisos);
            }
            context.BulkInsert(permisos);
            




        }
    }
}
