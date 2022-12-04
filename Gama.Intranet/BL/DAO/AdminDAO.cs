using Gama.Intranet.BL.DTO.Response;
using Gama.Intranet.BL.Models;
using System.Collections.Generic;

namespace Gama.Intranet.BL.DAO
{
    public interface AdminDAO : CRUD<Usuario>
    {
        string ResetPasswordUser(int Id);
        List<Usuario> GetUsers();
        List<Usuario> GetAllUsers();
        List<Usuario> GetAllUsersActive();
        List<Roles> GetRoles();
        List<LogStatus> GetStatus();
        List<PermissionsResponseDTO> GetPermissionsFolders(int IdUser);

        void UpdatePermissionsUser(List<UpdatePermissionsDAO> obj);

        int GetIdCategoria(string categoria);
        int GetIdFolder(string folder);

        string GetNameCategoria(int categoria);
        string GetNameFolder(int folder);


    }
}
