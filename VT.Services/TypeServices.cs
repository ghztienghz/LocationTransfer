using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
using VT.Repository;
namespace VT.Services
{
    public class TypeServices : ITypeServices
    {
        private LocationEntities db = new LocationEntities();
        private readonly ITypeRepository Type;
        public TypeServices()
        {
            this.Type = new TypeRepository(db);
        }
        public Model.Type AddObject(Model.Type obj)
        {
            return Type.AddObject(obj);
        }

        public IEnumerable<Model.Type> AddRangeObject(IEnumerable<Model.Type> multiObject)
        {
            return Type.AddRangeObject(multiObject);
        }

        public Model.Type DeleteObject(Model.Type obj)
        {
            return Type.DeleteObject(obj);
        }

        public IEnumerable<Model.Type> DeleteRangeObject(IEnumerable<Model.Type> multiObject)
        {
            return Type.DeleteRangeObject(multiObject);
        }

        public void Dispose()
        {
            Type.Dispose();
        }

        public IQueryable<Model.Type> FindAllObject(Expression<Func<Model.Type, bool>> func)
        {
            return Type.FindAllObject(func);
        }

        public Model.Type FindObject(Expression<Func<Model.Type, bool>> func)
        {
            return Type.FindObject(func);
        }

        public IQueryable<Model.Type> GetAllObject()
        {
            return Type.GetAllObject();
        }

        public Model.Type UpdateObject(Model.Type obj)
        {
            return Type.UpdateObject(obj);
        }
    }
}
