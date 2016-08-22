using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT.Model;

namespace VT.Repository
{
    public class GeoAreaRepository : Repository<GeoArea>, IGeoAreaRepository
    {
        public GeoAreaRepository(LocationEntities _db) : base(_db)
        {
        }
    }
}
