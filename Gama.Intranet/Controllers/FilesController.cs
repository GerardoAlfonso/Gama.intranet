using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.DTO.Response;
using Microsoft.AspNetCore.Cors;
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

        public FilesController(FilesDAO filesDAO)
        {
            this.filesDAO = filesDAO;
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
                path.Add(filesDAO.GetPublicPath("PublicPath").Value);
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
        [Route("GetPublicFiles")]
        public IActionResult GetPublicFiles() 
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                //string route = RoutePublicFiles(null);
                string route = @"C:\Users\";
                // get host route
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                fileResponseDTO.Status = 1;
                fileResponseDTO.Message = "Success";
                fileResponseDTO.Folders = GetFolders(path);
                fileResponseDTO.Files = GetFiles(path);

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

        [HttpPost]
        [Route("GetFilesToFolder")]
        public IActionResult GetFilesToFolder(GetFilesDTO filesDTO)
        {
            FileResponseDTO fileResponseDTO = new FileResponseDTO();
            try
            {
                string route = filesDTO.Route;
                // get host route
                fileResponseDTO.Folders = GetFolders(route);
                fileResponseDTO.Files = GetFiles(route);

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
    }
}

