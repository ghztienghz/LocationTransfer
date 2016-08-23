using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VT.Services;
using VT.Model.ViewModel;
using VT.Model.Ultil;
namespace VT.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly IPostItemServices PostItemServices;
        private readonly IMediaContentServices MediaContentServices;
        public LocationController(IPostItemServices _PostItemServices, IMediaContentServices _MediaContentServices)
        {
            this.PostItemServices = _PostItemServices;
            this.MediaContentServices = _MediaContentServices;
        }
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Root()
        {
            return View();
        }

        public ActionResult Details(long id,string FriendlyUrl)
        {
            LocationDetailViewModel vm = new LocationDetailViewModel();
            var post = PostItemServices.FindObject(x => x.Id == id && x.FriendlyUrl.Trim().ToLower() == FriendlyUrl.Trim().ToLower());
            if (post == null)
            {
                return RedirectToAction("Index", "Home");
            }
            post.CountView += 1;
            vm.Post = XObjectConvertToYObject.PostItemToPostItemViewModel(post);
            vm.LstMedia = MediaContentServices.FindAllObject(x => x.IdItem == post.Id && x.ObjectTypeId == (int)EnumCore.ObjectTypeId.LoaiMedia && x.IdType == (int)EnumCore.TypeMediaContent.HinhAnhBaiDang).ToList();
            PostItemServices.UpdateObject(post);
            return View(vm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PostItemServices.Dispose();
                MediaContentServices.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}