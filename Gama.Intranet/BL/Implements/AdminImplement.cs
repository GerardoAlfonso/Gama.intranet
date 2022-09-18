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
            throw new System.NotImplementedException();
        }

        public List<Usuario> getAll()
        {
            return context.Usuario.ToList();
        }

        public Usuario getById(long id)
        {
            throw new System.NotImplementedException();
        }

        public int insert(Usuario entity)
        {
            throw new System.NotImplementedException();
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
            return context.Usuario.Where(x => x.Status == 1).ToList();
        }
        public List<Usuario> GetAllUsers()
        {
            return context.Usuario.ToList();
        }

        public int update(Usuario DBEntity, Usuario entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
