using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Services
{
    public interface IObjectTypeServices
    {
        IQueryable<ObjectType> GetAllObject();

        IQueryable<ObjectType> FindAllObject(Expression<Func<ObjectType, bool>> func);

        ObjectType FindObject(Expression<Func<ObjectType, bool>> func);

        ObjectType AddObject(ObjectType obj);

        ObjectType UpdateObject(ObjectType obj);

        ObjectType DeleteObject(ObjectType obj);
        IEnumerable<ObjectType> DeleteRangeObject(IEnumerable<ObjectType> multiObject);
        void Dispose();
    }
}
