using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
using System.Linq;

namespace Gama.Intranet.BL.Implements
{
    public class FilesImplement : FilesDAO
    {
        private readonly ApplicationDBContext context;

        public FilesImplement(ApplicationDBContext context)
        {
            this.context = context;
        }

        public ParametrosGenerales GetPrivatePath(string type)
        {
            return (ParametrosGenerales)context.ParametrosGenerales.Where(x => x.Name == type);
        }

        public ParametrosGenerales GetPublicPath(string type)
        {
            ParametrosGenerales param = context.ParametrosGenerales.FirstOrDefault(x => x.Name == type);
            return param;
        }
    }
}
