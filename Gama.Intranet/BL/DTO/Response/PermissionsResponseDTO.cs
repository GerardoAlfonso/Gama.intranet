using System;

namespace Gama.Intranet.BL.DTO.Response
{
    public class PermissionsResponseDTO
    {
        public int Id { get; set; }
        public string Folder { get; set; }
        public string Categoria { get; set; }
        public Boolean Read { get; set; }
        public Boolean Write { get; set; }
    }
}
