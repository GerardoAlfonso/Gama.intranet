using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Gama.Intranet.BL.Implements
{
    public class ContentHtmlImplement : ContentHtmlDAO
    {
        private readonly ApplicationDBContext context;

        public ContentHtmlImplement(ApplicationDBContext context)
        {
            this.context = context;
        }

        public List<ContenidoHtml> GetContent(LoadContentHtmlDTO entity)
        {
            return context.ContenidoHtml.Where(x => x.IdPage == entity.IdPage && x.Name == entity.Name).ToList();
        }
    }
}
