using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.DTO.Response;
using Gama.Intranet.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gama.Intranet.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FilesDAO filesDAO;
        private readonly IWebHostEnvironment environment;
        

        public FilesController(FilesDAO filesDAO, IWebHostEnvironment hostEnvironment)
        {
            this.filesDAO = filesDAO;
            environment = hostEnvironment;
        }

        //
        [HttpGet]
        [Route("GetPublicPath")]
        public IActionResult GetPah()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                List<string> path = new List<string>();
                // production
                
                // develop
                path.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = path;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex;
                dto.Data = null;
            }
            return Ok(dto);
        }


        [HttpGet]
        [Authorize(Roles = "1,0")]
        [Route("GetPrivateFiles")]
        public IActionResult GetPrivateFiles()
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                // production
                //string path = RoutePublicFiles(null);
                //string route = @"C:\Users\";
                
                // get host route
                // develoop
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var folders = GetFolders(path);
                var files = GetFiles(path);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    //var a = Path.Combine(Path.GetDirectoryName(item), item);
                    //dirName.Add(Path.GetFileName(Path.GetDirectoryName(item)));
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }
                


                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;

            }
            catch(Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
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

        public string[] GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path); 
            return files;
        }

        [HttpPost]
        [Route("GetFileRoute")]
        public IActionResult GetFileRoute([FromBody] Downloadfile file)
        {
            var path = RoutePublicFiles("");
            var filepath = path + "\\" + file.Name;
            return Ok(filepath);
        }


        //[HttpPost("name:string")]
        [Route("DownloadPublic")]
        public IActionResult Download(string name)
        {
            // production
            //var path = RoutePublicFiles("");

            // develop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filepath = path + "\\" + name;
            var cadena = name.Substring((name.Length - 4), 4);
            
            if (cadena == ".pdf") 
            {
                return File(System.IO.File.ReadAllBytes(filepath), "document/pdf", System.IO.Path.GetFileName(filepath));
            }
            else if(cadena == ".png")
            {
                return File(System.IO.File.ReadAllBytes(filepath), "image/png", System.IO.Path.GetFileName(filepath));
            }
            else if (cadena == "docx")
            {
                return File(System.IO.File.ReadAllBytes(filepath), "document/docx", System.IO.Path.GetFileName(filepath));
            }
            else
            {
                return File(System.IO.File.ReadAllBytes(filepath), "document/*", System.IO.Path.GetFileName(filepath));
            }

        }


        [HttpGet]
        [Route("GetFolderCategories")]
        public IActionResult GetFolderCategories()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                List<FoldersCategories> path = new List<FoldersCategories>();
                // production
                path = filesDAO.GetCategories();

                // develop
                //path.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = path;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex;
                dto.Data = null;
            }
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        [Route("FoldersFromCategories")]
        public IActionResult FoldersFromCategories([FromBody] Folders fc)
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                var folders = filesDAO.FoldersFromCategories(fc.Id);

                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = folders;

            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }


        #region RRHH

        [HttpGet]
        [Route("GetPublicFiles")]
        public IActionResult GetPublicFiles()
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                // production
                //string path = RoutePublicFiles(null);
                //string route = @"C:\Users\";

                // get host route
                // develoop
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                path = path + "\\GAMA\\Private";
                var folders = GetFolders(path);
                var files = GetFiles(path);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    //var a = Path.Combine(Path.GetDirectoryName(item), item);
                    //dirName.Add(Path.GetFileName(Path.GetDirectoryName(item)));
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }



                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;

            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }

        [HttpGet]
        [Route("GetRRHHFiles")]
        public IActionResult GetRRHHFiles()
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                // production
                //string path = RoutePublicFiles(null);
                //string route = @"C:\Users\";

                // get host route
                // develoop
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                path = path + "\\GAMA\\Public\\RRHH";
                var folders = GetFolders(path);
                var files = GetFiles(path);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    //var a = Path.Combine(Path.GetDirectoryName(item), item);
                    //dirName.Add(Path.GetFileName(Path.GetDirectoryName(item)));
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }



                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;

            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }

        [HttpGet]
        [Route("GetDocumentosFiles")]
        public IActionResult GetDocumentosFiles()
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                // production
                //string path = RoutePublicFiles(null);
                //string route = @"C:\Users\";

                // get host route
                // develoop
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                path = path + "\\GAMA\\Public\\Documentos";
                var folders = GetFolders(path);
                var files = GetFiles(path);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    //var a = Path.Combine(Path.GetDirectoryName(item), item);
                    //dirName.Add(Path.GetFileName(Path.GetDirectoryName(item)));
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }



                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;

            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }

        [HttpGet]
        [Route("GetRecursosFiles")]
        public IActionResult GetRecursosFiles()
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                // production
                //string path = RoutePublicFiles(null);
                //string route = @"C:\Users\";

                // get host route
                // develoop
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                path = path + "\\GAMA\\Public\\Recursos";
                var folders = GetFolders(path);
                var files = GetFiles(path);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    //var a = Path.Combine(Path.GetDirectoryName(item), item);
                    //dirName.Add(Path.GetFileName(Path.GetDirectoryName(item)));
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }



                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;

            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }




        [HttpPost]
        [Route("GetPublicFilesToFolder")]
        public IActionResult GetPublicFilesToFolder(GetFilesDTO filesDTO)
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                string route = filesDTO.Route;
                // get host route


                var folders = GetFolders(route);
                var files = GetFiles(route);
                List<string> dirName = new List<string>();
                foreach (var item in folders)
                {
                    dirName.Add(Path.GetFileName(item));
                }
                List<string> fileName = new List<string>();
                foreach (var item in files)
                {
                    fileName.Add(Path.GetFileName(item));
                }

                fileResponseDTO.Folders = dirName;
                fileResponseDTO.Files = fileName;
                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";


            }
            catch (Exception ex)
            {
                fileResponseDTO.Status = 0;
                fileResponseDTO.Message = "Error: " + ex.Message;
                fileResponseDTO.Folders = null;
                fileResponseDTO.Files = null;
            }

            return Ok(fileResponseDTO);
        }



        #endregion 

    }
}

