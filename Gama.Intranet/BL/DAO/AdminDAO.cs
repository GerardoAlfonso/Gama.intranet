using Gama.Intranet.BL.Models;

namespace Gama.Intranet.BL.DAO
{
    public interface AdminDAO : CRUD<Usuario>
    {
        string ResetPasswordUser(int Id);

    }
}
