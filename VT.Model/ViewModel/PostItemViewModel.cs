using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace VT.Model.ViewModel
{
    public class PostItemViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống danh mục đăng tin")]
        [Display(Name ="Danh mục")]
        [RegularExpression("[0-9]+",ErrorMessage ="Chưa chọn tỉnh thành")]
        public long IdType { get; set; }
        public string TypeName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống loại đăng tin")]
        [Display(Name = "Loại tin")]
        [RegularExpression("[0-9]+", ErrorMessage = "Chọn loại đăng tin")]
        public long IdTypeNews { get; set; }
        public string TypeNewsName { get; set; }
        public long TypeParent { get; set; }
        public string TypeParentName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tỉnh thành")]
        [Display(Name = "Tỉnh - TP")]
        [RegularExpression("[0-9]+", ErrorMessage = "Chưa chọn tỉnh - thành")]
        public long IdProvince { get; set; }
        public string PovinceName { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống quận huyện")]
        [Display(Name = "Quận - Huyện")]
        [RegularExpression("[0-9]+", ErrorMessage = "Chưa chọn quận - huyện")]
        public long IdDistrict { get; set; }
        public string DistrictName { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống phường - xã")]
        [Display(Name = "Phường - Xã")]
        [RegularExpression("[0-9]+", ErrorMessage = "Chưa chọn phường xã")]
        public long IdWard { get; set; }
        public string WardName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống đơn vị tính")]
        [Display(Name = "Đơn vị tính")]
        [RegularExpression("[0-9]+", ErrorMessage = "Chưa chọn đơn vị tính")]
        public long Unit { get; set; }
        public string UnitName { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống giá")]
        [Display(Name = "Giá tiền")]
        public Nullable<double> Price { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tiêu đề")]
        [Display(Name = "Tiêu đề")]
        [MaxLength(100,ErrorMessage ="Tối đa 100 ký tự")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tên người lên hệ")]
        [Display(Name = "Tên người liên hệ")]
        public string NameContact { get; set; }
        [Display(Name = "Thể loại")]
        public List<ItemCheckBox> Model { get; set; }
        [Display(Name = "Vị trí")]
        public List<ItemCheckBox> Location { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống chiều ngang")]
        [Display(Name = "Chiều ngang")]
        public Nullable<double> Horizontal { get; set; }
        [Required(ErrorMessage ="Không được bỏ trống chiều dài")]
        [Display(Name = "Chiều dài")]
        public Nullable<double> Vertical { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống địa chỉ")]
        [Display(Name = "Địa chỉ")]
        [MaxLength(500,ErrorMessage ="Tối đa 500 ký tự")]
        public string FullAddress { get; set; }

        [Required(ErrorMessage = "Vui lòng đợi google maps tải vĩ độ")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "Vui lòng đợi google maps tải kinh độ")]
        public string Lng { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống số phòng")]
        [Display(Name = "Số phòng")]
        public string RoomNum { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống sô toilet")]
        [Display(Name = "Số phòng toilet")]
        public string Toilet { get; set; }
        [Display(Name = "Hiệu lực hợp đồng bằng tháng(vd : 1)")]
        [RegularExpression("[0-9]+", ErrorMessage = "Thời hạn hợp đồng phải là số")]
        public Nullable<int> Exprires { get; set; }
        public string Avatar { get; set; }
        public DateTime DateCreate { get; set; }
        public bool CreateByAdmin { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống điện thoại liên hệ")]
        [Display(Name = "Số điện thoại")]
        public string PhoneContact { get; set; }
        public string FriendlyUrl { get; set; }
        public string MetaAuthor { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống từ khoá dành cho SEO")]
        [Display(Name = "Từ khoá SEO google")]
        [MaxLength(100,ErrorMessage ="Tối đa 100 ký tự")]
        public string MetaSeoKeyword { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống mô tả dành cho SEO")]
        [Display(Name = "Từ khoá mô tả")]
        [MaxLength(200, ErrorMessage = "Tối đa 150 ký tự")]
        public string MetaSeoDescription { get; set; }

        public IEnumerable<SelectListItem> lstDropdownType { get; set; }

        public IEnumerable<SelectListItem> lstDropdownTypeNews { get; set; }

        public IEnumerable<SelectListItem> DropDownProvince { get; set; }
        public IEnumerable<SelectListItem> DropDownDistrict { get; set; }
        public IEnumerable<SelectListItem> DropDownWard { get; set; }

        [Display(Name = "Hình ảnh(tối đa 6 ảnh chất lượng cao)")]
        public List<HttpPostedFileBase> Files { get; set; }
    }

    public class ItemCheckBox
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
    public class UrlImage
    {
        public string Thumb { get; set; }
        public string FullSizeImg { get; set; }
    }
}
