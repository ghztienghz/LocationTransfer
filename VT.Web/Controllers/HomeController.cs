using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VT.Model;
using VT.Model.CustomSystem;
using VT.Model.Ultil;
using VT.Model.ViewModel;
using VT.Services;
namespace VT.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGeoAreaServices GeoService;
        private readonly ITypeServices TypeServices;
        private readonly IPostItemServices PostServices;
        public HomeController(IGeoAreaServices _GeoService, ITypeServices _TypeServices, IPostItemServices _PostServices)
        {
            this.GeoService = _GeoService;
            this.TypeServices = _TypeServices;
            this.PostServices = _PostServices;

        }
        public ActionResult Index()
        {
            HomeIndexViewModel vm = new HomeIndexViewModel();
            vm.Menu = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.DanhMuc && x.Status == (int)EnumCore.StatusObjectType.KichHoat).ToList();
            vm.TinDacBiet = PostServices.FindAllObject(x=>x.IdTypeNews == (int)EnumCore.LoaiTin.TinDacBiet && x.Status == (int)EnumCore.StatusPostItem.DaDuyet)
                .Select(x=>new PostItemViewModel { Id = x.Id, Avatar = x.Avatar, Price = x.Price, UnitName = x.UnitName, Title = x.Title, FriendlyUrl = x.FriendlyUrl, DistrictName = x.DistrictName }).ToList();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Helper
        private AppCustomUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppCustomUserManager>();
            }
        }

        private AppCustomRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppCustomRoleManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}