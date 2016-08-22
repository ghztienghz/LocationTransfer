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
    public class GeoAreaServices : IGeoAreaServices
    {
        private LocationEntities db = new LocationEntities();
        private IGeoAreaRepository Geo;
        public GeoAreaServices()
        {
            Geo = new GeoAreaRepository(db);
        }

        public GeoArea AddObject(GeoArea obj)
        {
            return Geo.AddObject(obj);
        }

        public GeoArea DeleteObject(GeoArea obj)
        {
            return Geo.DeleteObject(obj);
        }

        public IEnumerable<GeoArea> DeleteRangeObject(IEnumerable<GeoArea> multiObject)
        {
            return Geo.DeleteRangeObject(multiObject);
        }

        public void Dispose()
        {
            Geo.Dispose();
        }

        public IQueryable<GeoArea> FindAllObject(Expression<Func<GeoArea, bool>> func)
        {
            return Geo.FindAllObject(func);
        }

        public GeoArea FindObject(Expression<Func<GeoArea, bool>> func)
        {
            return Geo.FindObject(func);
        }

        public IQueryable<GeoArea> GetAllObject()
        {
            return Geo.GetAllObject();
        }

        public GeoArea UpdateObject(GeoArea obj)
        {
            return Geo.UpdateObject(obj);
        }
    }
}
