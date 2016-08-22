using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Services
{
    public interface IMediaContentServices
    {
        IQueryable<MediaContent> GetAllObject();

        IQueryable<MediaContent> FindAllObject(Expression<Func<MediaContent, bool>> func);

        MediaContent FindObject(Expression<Func<MediaContent, bool>> func);

        MediaContent AddObject(MediaContent obj);

        MediaContent UpdateObject(MediaContent obj);

        MediaContent DeleteObject(MediaContent obj);
        IEnumerable<MediaContent> DeleteRangeObject(IEnumerable<MediaContent> multiObject);
        void Dispose();
    }
}
