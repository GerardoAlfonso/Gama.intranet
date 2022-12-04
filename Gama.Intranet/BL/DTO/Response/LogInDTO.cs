using System;

namespace Gama.Intranet.BL.DTO.Response
{
    public class LogInDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Boolean ShouldChangePassword { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public int IdUser { get; set; }
    }
}
