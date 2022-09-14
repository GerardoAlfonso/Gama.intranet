using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
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
            return context.Usuario.FirstOrDefault(x => x.Name == usuario.User);
        }

        public int update(Usuario DBEntity, Usuario entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
