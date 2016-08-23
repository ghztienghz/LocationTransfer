using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT.Model;
namespace VT.Model.ViewModel
{
    public class HomeIndexViewModel
    {
        public List<Type> Menu { get; set; }

        // data tin
        public List<PostItemViewModel> TinDacBiet { get; set; }
        public List<PostItemViewModel> TinHot { get; set; }
        public List<PostItemViewModel> TinVip { get; set; }
        // phân vùng có chứa border màu hồng bên phải
        public List<PostItemViewModel> VipRight { get; set; }
    }
}
