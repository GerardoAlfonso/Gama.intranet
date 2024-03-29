﻿
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Intranet.BL.DAO
{
    
    public interface AuthDAO : CRUD<Usuario>
    {
        Usuario getUserInfo(string username);
        Usuario LogIn(CheckInDTO usuario);
        int LogOut(Usuario usuario);
        Usuario ChangePassword(Usuario DBEntity, ChangePasswordDTO usuario);

        bool VerifyPermission(int IdUser, string folder);
    }
}
