using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VT.Model.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Không được bỏ trống tên đăng nhập")]
        [RegularExpression("\\w{5,100}",ErrorMessage ="Tên đăng nhập chỉ chứa những ký tự A-Z a-z 0-9")]
        [MaxLength(100,ErrorMessage ="Tối đa 100 ký tự")]
        [Remote("CheckExistUsername", "Account",HttpMethod ="POST",ErrorMessage = "Tài khoản đẵ tồn tại trong hệ thống")]
        [Display(Name ="Tên đăng nhập")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống email")]
        [EmailAddress(ErrorMessage ="Phải là định dạng email")]
        [Remote("CheckExistEmail", "Account", HttpMethod = "POST",ErrorMessage ="Email đã tồn tại")]
        [Display(Name ="Email của bạn")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        [RegularExpression("\\w{5,100}", ErrorMessage = "Mật khẩu chỉ chứa những ký tự A-Z a-z 0-9")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        [Display(Name ="Mật khẩu")]
        public string Password { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống nhập lại mật khẩu")]
        [RegularExpression("\\w{5,100}", ErrorMessage = "Mật khẩu chỉ chứa những ký tự A-Z a-z 0-9")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage ="Mật khẩu phải trùng khớp")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống Họ và Tên")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        [Display(Name ="Họ và Tên")]
        public string FullName { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống Số điện thoại")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        [RegularExpression("^[0]{1}[1-9]{1}[0-9]{8,9}$", ErrorMessage = "Phải đúng số điện thoại VN. Vd: 01283402377")]
        [Display(Name ="Số điện thoại")]
        public string Phone { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống Địa chỉ")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string FullAddress { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống Tỉnh - TP")]
        [RegularExpression("^[0-9]{1,}$", ErrorMessage = "Vui lòng chọn Tỉnh - TP")]
        [Display(Name = "Tỉnh - TP")]
        public long IdProvince { get; set; }



        [Required(ErrorMessage = "Không được bỏ trống Quận - Huyện")]
        [RegularExpression("^[0-9]{1,}$", ErrorMessage = "Vui lòng chọn Quận - Huyện")]
        [Display(Name = "Quận - Huyện")]
        public long IdDistrict { get; set; }




        [Required(ErrorMessage = "Không được bỏ trống Phường - Xã")]
        [RegularExpression("^[0-9]{1,}$", ErrorMessage = "Vui lòng chọn Phường - Xã")]
        [Display(Name = "Phường - Xã")]
        public long IdWard { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }

        public IEnumerable<SelectListItem> DropDownProvince { get; set; }
        public IEnumerable<SelectListItem> DropDownDistrict { get; set; }
        public IEnumerable<SelectListItem> DropDownWard { get; set; }
    }

    public class LoginViewModel
    {
        public string Account { get; set; }
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
