using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VT.Model.ViewModel;
using VT.Services;
using VT.Model.Ultil;
using System.IO;
using System.Drawing;
using VT.Model;
using VT.Model.CustomSystem;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Xml.Linq;

namespace VT.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IGeoAreaServices GetGeoServices;
        private readonly ITypeServices TypeServices;
        private readonly IPostItemServices PostItemServices;
        private readonly IMediaContentServices MediaContentService;
        public PostController(IGeoAreaServices _GetGeoServices, ITypeServices _TypeServices, IPostItemServices _PostItemServices, IMediaContentServices _MediaContentService)
        {
            this.GetGeoServices = _GetGeoServices;
            this.TypeServices = _TypeServices;
            this.PostItemServices = _PostItemServices;
            this.MediaContentService = _MediaContentService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PostItem()
        {
            PostItemViewModel vm = new PostItemViewModel();
            var lstDropDownProvince = new List<SelectListItem>();
            lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = "Chọn Tỉnh - Thành" });
            GetGeoServices.FindAllObject(x => x.AreaDistrictId == null && x.AreaParentId == null).OrderBy(x => x.AreaName).ToList().ForEach(x =>
            {
                lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = (x.AreaTypeName != null ? x.AreaTypeName.ToLower() : string.Empty) + " " + x.AreaName, Value = x.AreaID + "" });
            });
            vm.DropDownProvince = lstDropDownProvince;
            var lstDropDownDistrict = new List<SelectListItem>();
            lstDropDownDistrict.Add(new SelectListItem { Selected = true, Text = "Chọn Quận - Huyện" });

            var lstDropDownWard = new List<SelectListItem>();
            lstDropDownWard.Add(new SelectListItem { Selected = true, Text = "Chọn Phường - Xã" });
            vm.DropDownDistrict = lstDropDownDistrict;
            vm.DropDownWard = lstDropDownWard;
            vm.lstDropdownType = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.DanhMuc).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            vm.lstDropdownTypeNews = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.LoaiTin).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            vm.Model = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.MoHinh).Select(x => new ItemCheckBox { Name = x.Name, Id = x.Id }).ToList();
            vm.Location = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.ViTri).Select(x => new ItemCheckBox { Name = x.Name, Id = x.Id }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostItem(PostItemViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Files == null || vm.Files.Count <= 0)
                {
                    ModelState.AddModelError("", "Phải tải lên server từ 1-6 tấm ảnh cho bài đăng");
                    goto goCode;
                }

                if (vm.Files != null && vm.Files.Count() > 0)
                {
                    // kiểm tra tỉnh
                    GeoArea areaProvince = GetGeoServices.FindObject(x => x.AreaID == vm.IdProvince);
                    if (areaProvince == null)
                    {
                        ModelState.AddModelError("", "Tỉnh - TP không tồn tại");
                        goto goCode;
                    }
                    GeoArea areaDistrict = GetGeoServices.FindObject(x => x.AreaID == vm.IdDistrict);
                    if (areaDistrict == null)
                    {
                        ModelState.AddModelError("", "Quận - Huyện không tồn tại");
                        goto goCode;
                    }
                    GeoArea areaWard = GetGeoServices.FindObject(x => x.AreaID == vm.IdWard);
                    if (areaWard == null)
                    {
                        ModelState.AddModelError("", "Phường - Xã không tồn tại");
                        goto goCode;
                    }
                    // kiểm tra thể loại menu
                    VT.Model.Type typeMenu = TypeServices.FindObject(x => x.Id == vm.IdType);
                    if (typeMenu == null)
                    {
                        ModelState.AddModelError("", "Menu bạn chọn không tồn tại");
                        goto goCode;
                    }
                    // kiểm tra thể loại tin đăng
                    VT.Model.Type typeNews = TypeServices.FindObject(x => x.Id == vm.IdTypeNews);
                    if (typeNews == null)
                    {
                        ModelState.AddModelError("", "Loại tin bạn chọn không tồn tại");
                        goto goCode;
                    }
                    // kiểm tra đơn vị tính
                    VT.Model.Type typeUnit = TypeServices.FindObject(x => x.Id == vm.Unit);
                    if (typeUnit == null)
                    {
                        ModelState.AddModelError("", "Đơn vị tính bạn chọn không tồn tại");
                        goto goCode;
                    }

                    // loại hình
                    List<VT.Model.Type> lstModel = new List<Model.Type>();
                    foreach (var item in vm.Model)
                    {
                        if (item.Checked)
                        {
                            var type = TypeServices.FindObject(x => x.Id == item.Id);
                            if (type != null)
                                lstModel.Add(type);
                        }
                    }
                    lstModel = lstModel.GroupBy(x => x.Id).Select(x => x.First()).ToList();

                    // vị trí địa lý
                    List<VT.Model.Type> lstLocation = new List<Model.Type>();
                    foreach (var item in vm.Location)
                    {

                        if (item.Checked)
                        {
                            var type = TypeServices.FindObject(x => x.Id == item.Id);
                            if (type != null)
                                lstLocation.Add(type);
                        }
                    }
                    lstLocation = lstLocation.GroupBy(x => x.Id).Select(x => x.First()).ToList();
                    // lưu hình
                    // key là tám ảnh full
                    // value là tấm ảnh thumb
                    Dictionary<string, string> lstImgForItem = new Dictionary<string, string>();
                    string[] fileAllow = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    for (int i = 0; i < (vm.Files.Count() > 6 ? 6 : vm.Files.Count()); i++)
                    {
                        HttpPostedFileBase file = vm.Files[i];
                        if (file.ContentLength / 1024 <= 20480 && fileAllow.Contains(Path.GetExtension(file.FileName)))
                        {
                            string newName = DateTime.Now.Ticks + DateTime.Now.ToString("ddMMyyyy") + Path.GetFileName(file.FileName);
                            string fileFullSize = Ultil.SaveFileImg(newName, Server, file);
                            if (fileFullSize != null)
                            {
                                Image img = Image.FromStream(file.InputStream);
                                Size size = new Size(250, 250);
                                //string fileThumb = Ultil.CreateThumbnail(img, size, Server, newName);
                                //string fileThumb = Ultil.GetThumbImg(new Size(300,300),newName,Server);
                                string fileThumb = Ultil.GenerateThumbImg(new Size(170, 140), newName, Server);
                                if (fileThumb != null)
                                {
                                    lstImgForItem.Add(fileFullSize, fileThumb);
                                }
                                else
                                {
                                    System.IO.File.Delete(Server.MapPath("..\\Media") + "\\" + newName);
                                }
                            }
                        }
                    }
                    Model.PostItem post = new Model.PostItem();
                    post.IdType = typeMenu.Id;
                    post.TypeName = typeMenu.Name;
                    post.IdTypeNews = typeNews.Id;
                    post.TypeNewsName = typeNews.Name;
                    post.IdTypeParent = null;
                    post.TypeParentName = null;
                    post.IdProvince = areaProvince.AreaID;
                    post.ProvinceName = areaProvince.AreaTypeName + " " + areaProvince.AreaName;

                    post.IdDistrict = areaDistrict.AreaID;
                    post.DistrictName = areaDistrict.AreaTypeName + " " + areaDistrict.AreaName;

                    post.IdWard = areaWard.AreaID;
                    post.WardName = areaWard.AreaTypeName + " " + areaWard.AreaName;
                    post.Unit = typeUnit.Id;
                    post.UnitName = typeUnit.Name;
                    post.Price = vm.Price ?? 0;
                    post.Title = Ultil.RemoveTagHtml(vm.Title);
                    post.Description = Ultil.RemoveTagHtml(vm.Description);
                    string strModel = "<Items>";
                    // tạo xml
                    foreach (var item in lstModel)
                    {
                        strModel += string.Format("<Item><Id>{0}</Id><Name>{1}</Name></Item>", item.Id, item.Name);
                    }
                    strModel += "</Items>";
                    if (!string.IsNullOrEmpty(strModel))
                        post.Model = strModel;

                    string strLocation = "<Items>";
                    foreach (var item in lstLocation)
                    {
                        strLocation += string.Format("<root><Item><Id>{0}</Id><Name>{1}</Name></Item></riit>", item.Id, item.Name);
                    }
                    strLocation += "</Items>";
                    if (!string.IsNullOrEmpty(strLocation))
                        post.Location = strModel;
                    post.Lat = vm.Lat;
                    post.Lng = vm.Lng;
                    post.RoomNum = vm.RoomNum;
                    post.Toilet = vm.Toilet;
                    post.Exprires = vm.Exprires ?? 0;
                    post.Avatar = lstImgForItem.First().Value;
                    post.DateCreate = DateTime.Now;
                    post.Status = (int)EnumCore.StatusPostItem.ChoDuyet;
                    post.PhoneContact = vm.PhoneContact;
                    post.FriendlyUrl = Ultil.FriendlyUrl(vm.Title);
                    AppIdentityUser user = UserManager.FindById(User.Identity.GetUserId<long>());
                    post.MetaAuthor = user.FullName ?? "Vô danh";
                    post.MetaSeoKeyword = vm.MetaSeoKeyword;
                    post.MetaSeoDescription = vm.MetaSeoDescription;
                    post.Horizontal = vm.Horizontal;
                    post.Vertical = vm.Vertical;
                    post.FullAddress = vm.FullAddress;

                    // dò tìm parent tỗng của danh mục
                    var parentId = GetParentId(typeMenu.Id);
                    if (typeMenu.Id != parentId.Id)
                    {
                        post.IdTypeParent = parentId.Id;
                        post.TypeParentName = parentId.Name;
                    }
                    // thêm vào database
                    post = PostItemServices.AddObject(post);
                    List<MediaContent> lstMediaContent = new List<MediaContent>();
                    Model.Type media = TypeServices.FindObject(x => x.Id == (int)EnumCore.TypeMediaContent.HinhAnhBaiDang && x.ObjectTypeId == (int)EnumCore.ObjectTypeId.LoaiMedia);
                    var checkImgFirst = true;
                    foreach (var item in lstImgForItem)
                    {
                        MediaContent m = new MediaContent();
                        m.IdType = media.Id;
                        m.TypeName = media.Name;
                        m.ObjectTypeId = media.ObjectTypeId;
                        m.ObjectName = media.ObjectTypeName;
                        m.IdItem = post.Id;
                        m.ItemName = post.Title;
                        m.Urlthumbnail = item.Value;
                        m.UrlFull = item.Key;
                        m.Status = (int)EnumCore.StatusMediaContent.ChoPhep;
                        if (checkImgFirst)
                        {
                            m.Active = true;
                            checkImgFirst = false;
                        }
                        lstMediaContent.Add(m);
                    }
                    MediaContentService.AddRangeObject(lstMediaContent);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Bạn chưa tải hình của bài viết lên server");
            }
            goCode: var lstDropDownProvince = new List<SelectListItem>();
            lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = "Chọn Tỉnh - Thành" });
            GetGeoServices.FindAllObject(x => x.AreaDistrictId == null && x.AreaParentId == null).OrderBy(x => x.AreaName).ToList().ForEach(x =>
            {
                lstDropDownProvince.Add(new SelectListItem { Selected = true, Text = (x.AreaTypeName != null ? x.AreaTypeName.ToLower() : string.Empty) + " " + x.AreaName, Value = x.AreaID + "" });
            });
            vm.DropDownProvince = lstDropDownProvince;
            var lstDropDownDistrict = new List<SelectListItem>();
            lstDropDownDistrict.Add(new SelectListItem { Selected = true, Text = "Chọn Quận - Huyện" });

            var lstDropDownWard = new List<SelectListItem>();
            lstDropDownWard.Add(new SelectListItem { Selected = true, Text = "Chọn Phường - Xã" });
            vm.DropDownDistrict = lstDropDownDistrict;
            vm.DropDownWard = lstDropDownWard;
            // type
            vm.lstDropdownType = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.DanhMuc).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            vm.lstDropdownTypeNews = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.LoaiTin).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            vm.Model = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.MoHinh).Select(x => new ItemCheckBox { Name = x.Name }).ToList();
            vm.Location = TypeServices.FindAllObject(x => x.ObjectTypeId == (int)EnumCore.ObjectTypeId.ViTri).Select(x => new ItemCheckBox { Name = x.Name }).ToList();
            return View(vm);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetGeoServices.Dispose();
                PostItemServices.Dispose();
                TypeServices.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Helpers

        // lấy parent id
        private Model.Type GetParentId(long id)
        {
            var obj = TypeServices.FindObject(x => x.Id == id);
            if (obj.ParentId == null)
            {
                return obj;
            }
            else
            {
                return GetParentId(obj.Id);
            }
        }
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