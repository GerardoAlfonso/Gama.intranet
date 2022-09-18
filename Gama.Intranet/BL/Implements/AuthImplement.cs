using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
using Gama.Intranet.Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gama.Intranet.BL.Implements
{
    public class AuthImplement : AuthDAO
    {
        private readonly ApplicationDBContext context;

        public AuthImplement(ApplicationDBContext context)
        {
            this.context = context;
        }

        public int deleteById(Usuario entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Usuario> getAll()
        {
            throw new System.NotImplementedException();
        }

        public Usuario getById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Usuario getUserInfo(string username)
        {
            return context.Usuario.FirstOrDefault(x => x.Name == username);
        }

        public int insert(Usuario entity)
        {
            throw new System.NotImplementedException();
        }

        public Usuario LogIn(CheckInDTO usuario)
        {
            Usuario user = context.Usuario.FirstOrDefault(x => x.Name == usuario.User && x.Password == usuario.Password && x.Status == 1);
            if (user != null)
            {
                user.LastAccess = DateTime.Now;
                user.LoginAttempts = 0;
                user.Token = Crypto.CreateJWT(user.Name, (int)user.Role);

                context.SaveChanges();
                return user;
            }
            else
            {
                Usuario count = context.Usuario.FirstOrDefault(x => x.Name == usuario.User && x.Status == 1);
                if (count.LoginAttempts == 2)
                {
                    count.LoginAttempts = 3;
                    count.LastAttempDate = DateTime.Now;
                    //count.Status = 0;
                    context.SaveChanges();
                }
                else
                {
                    count.LoginAttempts = (count.LoginAttempts != null ? count.LoginAttempts : 0) + 1;
                    context.SaveChanges();
                }
                return count;
            }
        }

        public int LogOut(Usuario usuario)
        {
            usuario.Token = null;
            context.SaveChanges();
            return usuario.Id;
        }

        public Usuario ChangePassword(Usuario DBEntity, ChangePasswordDTO usuario)
        {
            DBEntity.Password = usuario.Password;
            DBEntity.Token = Crypto.CreateJWT(DBEntity.Name, (int)DBEntity.Role);
            DBEntity.LastAccess = DateTime.Now;
            DBEntity.LoginAttempts = 0;
            DBEntity.ShouldChangePassword = false;

            context.SaveChanges();

            return DBEntity;
        }

        public int update(Usuario DBEntity, Usuario entity)
        {
            throw new System.NotImplementedException();
        }

       
    }
}
