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
    public class MediaContentServices : IMediaContentServices
    {
        private LocationEntities db = new LocationEntities();
        private readonly IMediaContentRepository Media;
        public MediaContentServices()
        {
            this.Media = new MediaContentRepository(db);
        }
        public MediaContent AddObject(MediaContent obj)
        {
            return Media.AddObject(obj);
        }

        public MediaContent DeleteObject(MediaContent obj)
        {
            return Media.DeleteObject(obj);
        }

        public IEnumerable<MediaContent> DeleteRangeObject(IEnumerable<MediaContent> multiObject)
        {
            return Media.DeleteRangeObject(multiObject);
        }

        public void Dispose()
        {
            Media.Dispose();
        }

        public IQueryable<MediaContent> FindAllObject(Expression<Func<MediaContent, bool>> func)
        {
            return Media.FindAllObject(func);
        }

        public MediaContent FindObject(Expression<Func<MediaContent, bool>> func)
        {
            return Media.FindObject(func);
        }

        public IQueryable<MediaContent> GetAllObject()
        {
            return Media.GetAllObject();
        }

        public MediaContent UpdateObject(MediaContent obj)
        {
            return Media.UpdateObject(obj);
        }
    }
}
