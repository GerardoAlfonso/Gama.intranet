using Gama.Intranet.BL.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Gama.Intranet.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FilesDAO filesDAO;

        public FilesController(FilesDAO filesDAO)
        {
            this.filesDAO = filesDAO;
        }

        [HttpPost]
        [Route("GetPublicFiles")]
        public IActionResult a() 
        {
            string route = RoutePublicFiles(null);
            GetFolders(route);
            GetFiles();
            return Ok(route);
        }

        public string RoutePublicFiles(string route)
        {
            var path = filesDAO.GetPublicPath("PublicPath");
            if(route == null)
            {
                return path.Value;
            }
            return path.Value + route;
        }
        public string RoutePrivateFiles(string route)
        {
            var path = filesDAO.GetPrivatePath("PrivatePath");
            if(route == null)
            {
                return path.Value;
            }
            return path.Value + route;
        }

        public string[] GetFolders(string path)
        {
            //string[] folders = Directory.GetDirectories(@"" + path);
            string[] folders = Directory.GetDirectories(path);
            return folders;
        }

        public string[] GetFiles()
        {
            string[] files = Directory.GetFiles(@"C:\Users\gerar\Desktop"); 
            return files;
        }
    }
}

