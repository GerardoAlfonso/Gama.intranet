using Gama.Intranet.BL.Models;

namespace Gama.Intranet.BL.DAO
{
    public interface FilesDAO
    {
        ParametrosGenerales GetPublicPath (string type);
        ParametrosGenerales GetPrivatePath (string type);
    }
}
