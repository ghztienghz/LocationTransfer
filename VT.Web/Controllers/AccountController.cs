using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VT.Model.CustomSystem;
using VT.Services;
using VT.Model;
using VT.Model.ViewModel;
using System.Collections.Generic;

namespace VT.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IGeoAreaServices GetGeoServices;
        public AccountController(IGeoAreaServices _GetGeoServices)
        {
            this.GetGeoServices = _GetGeoServices;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            var lstDropDownProvince = new List<SelectListItem>();
            lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = "Chọn Tỉnh - Thành" });
            GetGeoServices.FindAllObject(x => x.AreaDistrictId == null && x.AreaParentId == null).OrderBy(x => x.AreaName).ToList().ForEach(x =>
              {
                  lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = (x.AreaTypeName != null ? x.AreaTypeName.ToLower() : string.Empty) + " " + x.AreaName, Value = x.AreaID + "" });
              });


            var lstDropDownDistrict = new List<SelectListItem>();
            lstDropDownDistrict.Add(new SelectListItem { Selected = true, Text = "Chọn Quận - Huyện" });

            var lstDropDownWard = new List<SelectListItem>();
            lstDropDownWard.Add(new SelectListItem { Selected = true, Text = "Chọn Phường - Xã" });

            var r = new RegisterViewModel
            {
                DropDownProvince = lstDropDownProvince,
                DropDownDistrict = lstDropDownDistrict,
                DropDownWard = lstDropDownWard
            };
            return View(r);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel rg)
        {
            if (ModelState.IsValid)
            {
                var user = new AppIdentityUser
                {
                    UserName = rg.Username.Trim().ToLower(),
                    Email = rg.Email.Trim().ToLower(),
                    PasswordHash = UserManager.PasswordHasher.HashPassword(rg.Password),
                    FullName = rg.FullName,
                    PhoneNumber = rg.Phone,
                    Address = rg.FullAddress,
                    IdProvince = rg.IdProvince,
                    IdDistrict = rg.IdDistrict,
                    IdWard = rg.IdWard,
                    Lat = rg.Lat,
                    Lng = rg.Lng
                };
                var rs = await UserManager.CreateAsync(user);
                if (rs.Succeeded)
                {
                    var ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    rs.Errors.ToList().ForEach(x =>
                    {
                        ModelState.AddModelError("", x);
                    });
                }
            }
            var lstDropDownDistrict = new List<SelectListItem>();
            lstDropDownDistrict.Add(new SelectListItem { Selected = true, Text = "Chọn Quận - Huyện" });

            var lstDropDownWard = new List<SelectListItem>();
            lstDropDownWard.Add(new SelectListItem { Selected = true, Text = "Chọn Phường - Xã" });
            rg.DropDownDistrict = lstDropDownDistrict;
            rg.DropDownWard = lstDropDownWard;
            return View(rg);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            AuthenticationManager.SignOut();
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Account = vm.Account.Trim().ToLower();
                var user = UserManager.Find(vm.Account, vm.Password);
                if (user != null)
                {
                    var claim = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = vm.Remember }, claim);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    user = UserManager.FindByEmail(vm.Account.Trim().ToLower());
                    if (user != null && UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, vm.Password) == PasswordVerificationResult.Success)
                    {
                        var claim = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = vm.Remember }, claim);
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
            return View(vm);
        }

        // kiểm tra email trùng
        [AllowAnonymous]
        public ActionResult CheckExistUsername(string Username)
        {
            var user = UserManager.FindByName(Username);
            return Json(user == null, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult CheckExistEmail(string Email)
        {
            var user = UserManager.FindByEmail(Email);
            return Json(user == null, JsonRequestBehavior.AllowGet);
        }

        // lấy địa chỉ của quận huyện phường xã
        [AllowAnonymous]
        public ActionResult GetArea(long id)
        {
            object obj = GetGeoServices.FindAllObject(x => x.AreaParentId == id).OrderBy(x=>x.AreaName).Select(x => new
            {
                AreaId = x.AreaID,
                AreaName = (x.AreaTypeName != null ? x.AreaTypeName.ToLower() : "") + " " + x.AreaName
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region Quản lý
        public ActionResult Manager()
        {
            return View();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                GetGeoServices.Dispose();
            base.Dispose(disposing);
        }
        #region Helpers
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
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}