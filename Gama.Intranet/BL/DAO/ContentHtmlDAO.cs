using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.Models;
using System.Collections.Generic;

namespace Gama.Intranet.BL.DAO
{
    public interface ContentHtmlDAO
    {
        List<ContenidoHtml> GetContent(LoadContentHtmlDTO entity); 
    }
}
