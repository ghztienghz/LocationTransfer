using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VT.Model.CustomSystem;
namespace VT.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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