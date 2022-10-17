using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.DTO.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gama.Intranet.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    public class ContentHtmlController : ControllerBase
    {
        private readonly ContentHtmlDAO contentHtml;

        public ContentHtmlController(ContentHtmlDAO contentHtml)
        {
            this.contentHtml = contentHtml;
        }
        #region ContentHtml

        [HttpPost]
        [Route("GetContentHtml")]
        public IActionResult GetContentHtml(LoadContentHtmlDTO dto)
        {
            GenericDTO rp = new GenericDTO();

            var a = contentHtml.GetContent(dto);
            rp.Status = 1;
            rp.Message = "ok";
            rp.Data = a;

            return Ok(rp);
        }

        #endregion

    }
}
