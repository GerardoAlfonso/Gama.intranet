using System.Collections.Generic;

namespace Gama.Intranet.BL.DAO
{
    public interface CRUD<TEntity>
    {
        List<TEntity> getAll();
        TEntity getById(long id);
        int update(TEntity DBEntity, TEntity entity);
        int deleteById(TEntity entity);
        int insert(TEntity entity);
    }
}
