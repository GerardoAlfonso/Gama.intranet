using Gama.Intranet.BL.Models;

namespace Gama.Intranet.BL.DAO
{
    public interface LogsDAO
    {
        void WriteLog(Logs log);
    }
}
