using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private LocationEntities db;
        public Repository(LocationEntities _db)
        {
            this.db = _db;
        }
        public TEntity AddObject(TEntity obj)
        {
            try
            {
                TEntity add = db.Set<TEntity>().Add(obj);
                db.SaveChanges();
                return add;
            }
            catch (DbEntityValidationException e)
            {
                return null;
            }
        }

        public TEntity DeleteObject(TEntity obj)
        {
            TEntity remove = db.Set<TEntity>().Remove(obj);
            db.SaveChanges();
            return remove;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<TEntity> GetAllObject()
        {
            return db.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAllObject(Expression<Func<TEntity, bool>> func)
        {
            IQueryable<TEntity> get = db.Set<TEntity>().Where(func);
            return get;
        }

        public TEntity UpdateObject(TEntity obj)
        {
            try
            {
                db.Entry<TEntity>(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return obj;
            }
            catch (DbEntityValidationException e)
            {
                return null;
            }
        }

        public TEntity FindObject(Expression<Func<TEntity, bool>> func)
        {
            TEntity find = db.Set<TEntity>().SingleOrDefault(func);
            return find;
        }

        public IEnumerable<TEntity> DeleteRangeObject(IEnumerable<TEntity> multiObj)
        {
            IEnumerable<TEntity> lst = db.Set<TEntity>().RemoveRange(multiObj);
            return lst;
        }

        public IEnumerable<TEntity> AddRangeObject(IEnumerable<TEntity> multiObject)
        {
            try
            {
                var LstObj = db.Set<TEntity>().AddRange(multiObject);
                db.SaveChanges();
                return LstObj;
            }
            catch (DbEntityValidationException ex)
            {
                return null;
            }
        }
    }
}
