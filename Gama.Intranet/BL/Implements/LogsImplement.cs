using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.Models;
using Gama.Intranet.DAL;

namespace Gama.Intranet.BL.Implements
{
    public class LogsImplement : LogsDAO
    {
        private readonly ApplicationDBContext context;

        public LogsImplement(ApplicationDBContext context)
        {
            this.context = context;
        }
        public void WriteLog(Logs log)
        {
            context.Logs.Add(log);
            context.SaveChanges();
            
        }
    }
}
