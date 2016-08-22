using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllObject();

        IQueryable<TEntity> FindAllObject(Expression<Func<TEntity,bool>> func);

        TEntity FindObject(Expression<Func<TEntity, bool>> func);

        TEntity AddObject(TEntity obj);

        TEntity UpdateObject(TEntity obj);

        TEntity DeleteObject(TEntity obj);

        IEnumerable<TEntity> DeleteRangeObject(IEnumerable<TEntity> multiObj);

        void Dispose();
    }
}
