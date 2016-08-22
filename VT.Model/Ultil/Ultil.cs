using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Web;
using System.Text.RegularExpressions;

namespace VT.Model.Ultil
{
    public static class Ultil
    {
        public static string RemoveTagHtml(string text)
        {
            return Regex.Replace(text, @"<[^>]*>", String.Empty);
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string FriendlyUrl(string text)
        {
            try
            {
                string Url = RemoveUnicode(text).Trim().Replace(" ", "-");
                string FriendlyUrl = Regex.Replace(Url, "-+-", "-").ToLower();
                return FriendlyUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Lưu hình ảnh
        /// </summary>
        /// <param name="fileName">Tên của file</param>
        /// <param name="Server">Giúp cho việc lưu hình ảnh. HttpServerUtilityBase</param>
        /// <param name="file">Dữ liệu file cần lưu</param>
        /// <returns>Trả về một đường dẫn khi lưu</returns>
        public static string SaveFileImg(string fileName, HttpServerUtilityBase Server, HttpPostedFileBase file)
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("..\\Media")))
                {
                    Directory.CreateDirectory(Server.MapPath("..\\Media"));
                }
                var pathSave = Server.MapPath("..\\Media") + "\\" + fileName;
                file.SaveAs(pathSave);
                return "/Media/" + fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Nhận một tấn ảnh Thumbnail
        /// </summary>
        /// <param name="pathImg">Đây là đường dẫn tới file sẽ crop làm ảnh thumb</param>
        /// <param name="Server">HttpServerUtilityBase giúp cho việc lưu trữ tại thư mục trên ổ cứng Server</param>
        /// <returns>trẻ về đường dẫn</returns>
        public static string GetThumbImg(string pathImg, HttpServerUtilityBase Server)
        {
            try
            {
                Image image = Image.FromFile(pathImg);
                Image thumb = image.GetThumbnailImage(250, 250, () => false, IntPtr.Zero);
                string newFile = Server.MapPath("..\\Thumb") + "\\" + Path.GetFileName(pathImg);
                if (!Directory.Exists(Server.MapPath("..\\Thumb")))
                {
                    Directory.CreateDirectory(Server.MapPath("..\\Thumb"));
                }
                thumb.Save(newFile);
                return "/Thumb/" + Path.GetFileName(pathImg);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Tạo ảnh thum
        /// imThumbnailImage = CreateThumbnail(OriginalImage, new Size(width, height))
        /// </summary>
        /// <param name="image">Image image = Image.FromFile(pathImg)</param>
        /// <param name="thumbnailSize">Kích thước muốn cắt</param>
        /// <param name="Server">HttpServerUtilityBase Dùng để nhận full đường dẫn tới thư mục ổ đĩa.</param>
        /// <param name="fileName">tên của cái file thumb</param>
        /// <returns>trả về đường dẫn tương đối của tấm ảnh thumb đã lưu</returns>
        public static string CreateThumbnail(Image image, Size thumbnailSize, HttpServerUtilityBase Server, string fileName)
        {
            try
            {
                float scalingRatio = CalculateScalingRatio(image.Size, thumbnailSize);
                int scaledWidth = (int)Math.Round((float)image.Size.Width * scalingRatio);
                int scaledHeight = (int)Math.Round((float)image.Size.Height * scalingRatio);
                int scaledLeft = (thumbnailSize.Width - scaledWidth) / 2;
                int scaledTop = (thumbnailSize.Height - scaledHeight) / 2;

                // For portrait mode, adjust the vertical top of the crop area so that we get more of the top area
                if (scaledWidth < scaledHeight && scaledHeight > thumbnailSize.Height)
                {
                    scaledTop = (thumbnailSize.Height - scaledHeight) / 4;
                }

                Rectangle cropArea = new Rectangle(scaledLeft, scaledTop, scaledWidth, scaledHeight);

                System.Drawing.Image thumbnail = new Bitmap(thumbnailSize.Width, thumbnailSize.Height);
                using (Graphics thumbnailGraphics = Graphics.FromImage(thumbnail))
                {
                    thumbnailGraphics.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    thumbnailGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphics.DrawImage(image, cropArea);
                }
                if (!Directory.Exists(Server.MapPath("..\\Thumb")))
                {
                    Directory.CreateDirectory(Server.MapPath("..\\Thumb"));
                }
                thumbnail.Save(Server.MapPath("..\\Thumb") + "\\" + fileName);
                return "/Thumb/" + fileName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static float CalculateScalingRatio(Size originalSize, Size targetSize)
        {
            float originalAspectRatio = (float)originalSize.Width / (float)originalSize.Height;
            float targetAspectRatio = (float)targetSize.Width / (float)targetSize.Height;

            float scalingRatio = 0;

            if (targetAspectRatio >= originalAspectRatio)
            {
                scalingRatio = (float)targetSize.Width / (float)originalSize.Width;
            }
            else
            {
                scalingRatio = (float)targetSize.Height / (float)originalSize.Height;
            }

            return scalingRatio;
        }
    }
}
