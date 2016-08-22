using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT.Model;

namespace VT.Repository
{
    public class ObjectTypeRepository : Repository<ObjectType>, IObjectTypeRepository
    {
        public ObjectTypeRepository(LocationEntities _db) : base(_db)
        {
        }
    }
}
