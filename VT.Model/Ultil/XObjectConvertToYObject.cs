using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VT.Model.ViewModel;
namespace VT.Model.Ultil
{
    public static class XObjectConvertToYObject
    {
        public static PostItemViewModel PostItemToPostItemViewModel(PostItem post)
        {
            PostItemViewModel p = new PostItemViewModel();
            p.Id = post.Id;
            p.IdType = post.IdType;
            p.TypeName = post.TypeName;
            p.IdTypeNews = post.IdTypeNews ?? 0;
            p.TypeNewsName = post.TypeNewsName;
            p.TypeParent = post.IdTypeParent ?? 0;
            p.TypeParentName = post.TypeParentName;
            p.IdProvince = post.IdProvince;
            p.ProvinceName = post.ProvinceName;
            p.IdDistrict = post.IdDistrict;
            p.DistrictName = post.DistrictName;
            p.IdWard = post.IdWard;
            p.WardName = post.WardName;
            p.Unit = post.Unit;
            p.UnitName = post.UnitName;
            p.Price = post.Price;
            p.Title = post.Title;
            p.Description = post.Description;
            if (post.Model != null && post.Model.Trim().Length > 0)
            {
                p.Model = XDocument.Parse(post.Model).Elements("Item").Select(x => new ItemCheckBox { Id = long.Parse(x.Element("Id").Value), Name = x.Element("Name").Value }).ToList();
            }
            if (post.Location != null && post.Location.Trim().Length > 0)
            {
                p.Location = XDocument.Parse(post.Location).Elements("Item").Select(x => new ItemCheckBox { Id = long.Parse(x.Element("Id").Value), Name = x.Element("Name").Value }).ToList();
            }
            p.Lat = post.Lat;
            p.Lng = post.Lng;
            p.RoomNum = post.RoomNum;
            p.Toilet = post.Toilet;
            p.Exprires = post.Exprires;
            p.Avatar = post.Avatar;
            p.DateCreate = post.DateCreate;
            p.CreateByAdmin = post.CreateByAdmin;
            p.Status = post.Status;
            p.PhoneContact = post.PhoneContact;
            p.FriendlyUrl = post.FriendlyUrl;
            p.MetaAuthor = post.MetaAuthor;
            p.MetaSeoKeyword = post.MetaSeoKeyword;
            p.MetaSeoDescription = post.MetaSeoDescription;
            p.Horizontal = post.Horizontal;
            p.Vertical = post.Vertical;
            p.FullAddress = post.FullAddress;
            p.CountView = post.CountView;
            return p;
        }
    }
}
