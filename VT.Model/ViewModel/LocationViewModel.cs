using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT.Model.ViewModel
{
    public class LocationDetailViewModel
    {
        /// <summary>
        /// sản phẩm
        /// </summary>
        public PostItemViewModel Post { get; set; }
        /// <summary>
        /// media của bài đăng
        /// </summary>
        public List<MediaContent> LstMedia { get; set; }

        /// <summary>
        /// sản phẩm liên quan
        /// </summary>
        public List<PostItemViewModel> PostInvolve { get; set; }
    }
}
