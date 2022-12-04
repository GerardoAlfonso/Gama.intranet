using System;

namespace Gama.Intranet.BL.DAO
{
    public class UpdatePermissionsDAO
    {
        public int IdUser { get; set; }
        public bool lectura { get; set; }
        public bool escritura { get; set; }
        public string Folder { get; set; }
        public string Categoria{ get; set; }

    }
}
