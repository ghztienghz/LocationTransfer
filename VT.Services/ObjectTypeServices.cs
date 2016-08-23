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
    public class ObjectTypeServices : IObjectTypeServices
    {
        private LocationEntities db = new LocationEntities();
        private readonly IObjectTypeRepository ObjectType;
        public ObjectTypeServices()
        {
            this.ObjectType = new ObjectTypeRepository(db);
        }
        public ObjectType AddObject(ObjectType obj)
        {
            return ObjectType.AddObject(obj);
        }

        public IEnumerable<ObjectType> AddRangeObject(IEnumerable<ObjectType> multiObject)
        {
            return ObjectType.AddRangeObject(multiObject);
        }

        public ObjectType DeleteObject(ObjectType obj)
        {
            return ObjectType.DeleteObject(obj);
        }

        public IEnumerable<ObjectType> DeleteRangeObject(IEnumerable<ObjectType> multiObject)
        {
            return ObjectType.DeleteRangeObject(multiObject);
        }

        public void Dispose()
        {
            ObjectType.Dispose();
        }

        public IQueryable<ObjectType> FindAllObject(Expression<Func<ObjectType, bool>> func)
        {
            return ObjectType.FindAllObject(func);
        }

        public ObjectType FindObject(Expression<Func<ObjectType, bool>> func)
        {
            return ObjectType.FindObject(func);
        }

        public IQueryable<ObjectType> GetAllObject()
        {
            return ObjectType.GetAllObject();
        }

        public ObjectType UpdateObject(ObjectType obj)
        {
            return ObjectType.UpdateObject(obj);
        }
    }
}
