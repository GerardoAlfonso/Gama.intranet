using Gama.Intranet.BL.Models;
using System.Collections.Generic;

namespace Gama.Intranet.BL.DAO
{
    public interface FilesDAO
    {
        ParametrosGenerales GetPublicPath (string type);
        ParametrosGenerales GetPrivatePath (string type);

        List<FoldersCategories> GetCategories();
        List<Folders> FoldersFromCategories(int id);
    }
}
