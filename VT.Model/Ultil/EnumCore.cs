using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT.Model.Ultil
{
    public class EnumCore
    {
        public enum ObjectTypeId : int
        {
            DanhMuc = 1,
            LoaiTin = 2,
            DonViTinh = 3,
            MoHinh = 5,
            ViTri = 6
        }
        public enum LoaiTin : int
        {
            TinVip = 6,
            TinHot = 5,
            TinDacBiet = 4
        }

        public enum Unit : int
        {
            Trieu = 7,
            Ty = 8
        }
        public enum StatusTable : int
        {
            KichHoat = 1,
            Huy = 0
        }
        public enum StatusPostItem : int
        {
            ChoDuyet = 18,
            DaDuyet = 19
        }
    }
}
