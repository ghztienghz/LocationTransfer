using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Services
{
    public interface IPostItemServices
    {
        IQueryable<PostItem> GetAllObject();

        IQueryable<PostItem> FindAllObject(Expression<Func<PostItem, bool>> func);

        PostItem FindObject(Expression<Func<PostItem, bool>> func);

        PostItem AddObject(PostItem obj);
        IEnumerable<PostItem> AddRangeObject(IEnumerable<PostItem> multiObject);

        PostItem UpdateObject(PostItem obj);

        PostItem DeleteObject(PostItem obj);
        IEnumerable<PostItem> DeleteRangeObject(IEnumerable<PostItem> multiObject);
        void Dispose();
    }
}
