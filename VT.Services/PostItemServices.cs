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
    public class PostItemServices : IPostItemServices
    {
        private LocationEntities db = new LocationEntities();
        private readonly IPostItemRepository Post;
        public PostItemServices()
        {
            this.Post = new PostItemRepository(db);
        }
        public PostItem AddObject(PostItem obj)
        {
            return Post.AddObject(obj);
        }

        public IEnumerable<PostItem> AddRangeObject(IEnumerable<PostItem> multiObject)
        {
            return Post.AddRangeObject(multiObject);
        }

        public PostItem DeleteObject(PostItem obj)
        {
            return Post.DeleteObject(obj);
        }

        public IEnumerable<PostItem> DeleteRangeObject(IEnumerable<PostItem> multiObject)
        {
            return Post.DeleteRangeObject(multiObject);
        }

        public void Dispose()
        {
            Post.Dispose();
        }

        public IQueryable<PostItem> FindAllObject(Expression<Func<PostItem, bool>> func)
        {
            return Post.FindAllObject(func);
        }

        public PostItem FindObject(Expression<Func<PostItem, bool>> func)
        {
            return Post.FindObject(func);
        }

        public IQueryable<PostItem> GetAllObject()
        {
            return Post.GetAllObject();
        }

        public PostItem UpdateObject(PostItem obj)
        {
            return Post.UpdateObject(obj);
        }
    }
}
