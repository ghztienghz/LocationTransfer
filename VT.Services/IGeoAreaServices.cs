using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Services
{
    public interface IGeoAreaServices
    {
        IQueryable<GeoArea> GetAllObject();

        IQueryable<GeoArea> FindAllObject(Expression<Func<GeoArea, bool>> func);

        GeoArea FindObject(Expression<Func<GeoArea, bool>> func);

        GeoArea AddObject(GeoArea obj);
        IEnumerable<GeoArea> AddRangeObject(IEnumerable<GeoArea> multiObject);

        GeoArea UpdateObject(GeoArea obj);

        GeoArea DeleteObject(GeoArea obj);
        IEnumerable<GeoArea> DeleteRangeObject(IEnumerable<GeoArea> multiObject);
        void Dispose();
    }
}
