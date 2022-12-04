using EFCore.BulkExtensions;
using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Response;
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
        public List<PermissionsResponseDTO> GetPermissionsFolders(int IdUser)
        {
            var result = context.UsuariosPermisosFolders.Where(x => x.IdUsuario == IdUser).ToList();
            List<PermissionsResponseDTO> list = new List<PermissionsResponseDTO>();
            foreach(var item in result)
            {
                PermissionsResponseDTO obj = new PermissionsResponseDTO();
                obj.Id = item.Id;
                obj.Write = item.Write;
                obj.Read = item.Read;
                obj.Folder = GetNameFolder(item.IdFolder);
                obj.Categoria = GetNameCategoria(item.IdCategoria);
                
                list.Add(obj);
            }

            return list;
        }

        public void UpdatePermissionsUser(List<UpdatePermissionsDAO> permissions)
        {
            List<UsuariosPermisosFolders> list = new List<UsuariosPermisosFolders>();
            int idUser = 0;
            foreach (var item in permissions)
            {
                // validar si se esta recibiendo una carpeta o solo el id usuario
                idUser = item.IdUser;
                if (item.Categoria != null)
                {
                    UsuariosPermisosFolders obj = new UsuariosPermisosFolders();
                    obj.IdFolder = item.Folder == null ? 0 : GetIdFolder(item.Folder);
                    obj.IdCategoria = item.Categoria == null ? 0 : GetIdCategoria(item.Categoria);
                    obj.IdUsuario = item.IdUser;
                    obj.Write = item.escritura;
                    obj.Read = item.lectura;
                    list.Add(obj);
                }
                
            }

            // 
            var results = context.UsuariosPermisosFolders.Where(x => x.IdUsuario == idUser).ToList();
            List<UsuariosPermisosFolders> permisos = new List<UsuariosPermisosFolders>();
            foreach(var item in results)
            {
                context.UsuariosPermisosFolders.Remove(item);
            }
            // delete old permissions
            context.SaveChanges();

            // validar si se debe insertar permisos
            if (permissions != null)
            {
                context.BulkInsert(list);
            }
            

        }

        public int GetIdCategoria(string categoria)
        {
            var result = context.FoldersCategories.FirstOrDefault(x => x.Nombre == categoria);
            if(result == null)
            {
                return -1;
            }
            else
            {
                return result.Id;
            }
        }
        public int GetIdFolder(string folder)
        {
            var result = context.Folders.FirstOrDefault(x => x.Name == folder);
            if (result == null)
            {
                return -1;
            }
            else
            {
                return result.Id;
            }
        }

        public string GetNameCategoria(int categoria)
        {
            var result = context.FoldersCategories.FirstOrDefault(x => x.Id == categoria);
            if (result != null)
            {
                return result.Nombre;
            }
            else
            {
                return "";
            }
        }
        public string GetNameFolder(int folder)
        {
            var result = context.Folders.FirstOrDefault(x => x.Id == folder);
            if(result != null)
            {
                return result.Name;
            }
            else
            {
                return "";
            }
        }
    }
}
