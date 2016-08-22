using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Services
{
    public interface ITypeServices
    {
        IQueryable<Model.Type> GetAllObject();

        IQueryable<Model.Type> FindAllObject(Expression<Func<Model.Type, bool>> func);

        Model.Type FindObject(Expression<Func<Model.Type, bool>> func);

        Model.Type AddObject(Model.Type obj);

        Model.Type UpdateObject(Model.Type obj);

        Model.Type DeleteObject(Model.Type obj);
        IEnumerable<Model.Type> DeleteRangeObject(IEnumerable<Model.Type> multiObject);
        void Dispose();
    }
}
