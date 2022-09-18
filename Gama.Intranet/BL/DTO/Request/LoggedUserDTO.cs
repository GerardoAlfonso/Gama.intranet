using System;

namespace Gama.Intranet.BL.DTO.Request
{
    public class LoggedUserDTO
    {
        public string User { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
    }
}
