using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
using System.Collections.Generic;
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

        public List<Folders> FoldersFromCategories(int id)
        {
            return context.Folders.Where(x => x.IdCategoria == id).ToList();
        }

        public List<FoldersCategories> GetCategories()
        {
            List<FoldersCategories> categories = context.FoldersCategories.ToList();
            //List<string> list = new List<string>();

            //foreach (var item in categories)
            //{
            //    list.Add(item.Nombre);
            //}
            return categories;
        }

        public ParametrosGenerales GetPrivatePath(string type)
        {
            ParametrosGenerales param = context.ParametrosGenerales.FirstOrDefault(x => x.Name == type);
            return param;
        }

        public ParametrosGenerales GetPublicPath(string type)
        {
            ParametrosGenerales param = context.ParametrosGenerales.FirstOrDefault(x => x.Name == type);
            return param;
        }


    }
}
