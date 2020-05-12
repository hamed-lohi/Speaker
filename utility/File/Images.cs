using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utility.File
{
    public class Images
    {
        /// <summary>
        /// ذخیره تصاویر بیس64 به صورت فایل
        /// </summary>
        public static void SaveImage(string[] images, string newImageUrl, string oldImg, string newRecordImageUrl, string path, string domainName)
        {
            IList<byte[]> allowedFiles = new List<byte[]>();
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 });//jpg
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xDB });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 238 });
            allowedFiles.Add(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });//png
            allowedFiles.Add(new byte[] { 0x42, 0x4D });


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (images != null && !string.IsNullOrEmpty(newImageUrl))
            {
         
                if (!string.IsNullOrEmpty(oldImg))
                {
                    var image = oldImg.Split(',');

                    var deleteImagePath =
                        image.Where(i => string.IsNullOrEmpty(newRecordImageUrl) || !newRecordImageUrl.Contains(i))
                            .Select(i => i.Replace(domainName, path));

                    foreach (var img in deleteImagePath)
                    {
                        if (System.IO.File.Exists(img))
                        {
                            System.IO.File.Delete(img);
                        }  
                    }
                }

                var filePath = newImageUrl.Replace(domainName, path).Split(',');

                for (int k = 0; k < images.Length; k++)
                {
                    if (string.IsNullOrEmpty(images[k]))
                    {
                        break;
                    }

                    var flag = true;
                    var myImage = images[k].Replace(" ", "+");
                    byte[] bytes = Convert.FromBase64String(myImage);

                    //if (bytes.Length > 8700000) // 8 mb
                    //{
                    //    throw new Exception(ResourceMessage.Message.InvalidFileSize);
                    //}

                    //foreach (var fileHead in allowedFiles)
                    //{
                    //    flag = !fileHead.Where((t, i) => t != bytes[i]).Any();

                    //    if (flag)
                    //    {
                    //        break;
                    //    }
                    //}

                    //if (!flag)
                    //{
                    //    throw new Exception(ResourceMessage.Message.InvalidFileType);
                    //}

                    System.IO.File.WriteAllBytes(filePath[k], bytes);
                }

            }

            //return 0;
        }

        /// <summary>
        /// ذخیره فایل بیس64 به صورت فایل فیزیکی
        /// </summary>
        public static void SaveFile(string file, string newImageUrl, string oldImg, string path, string domainName)
        {
            IList<byte[]> allowedFiles = new List<byte[]>();
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 });//jpg
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 0xDB });
            allowedFiles.Add(new byte[] { 0xFF, 0xD8, 0xFF, 238 });
            allowedFiles.Add(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });//png
            allowedFiles.Add(new byte[] { 0x42, 0x4D });


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null && !string.IsNullOrEmpty(file) && !string.IsNullOrEmpty(newImageUrl))
            {

                DeleteFile(oldImg, domainName, path);

                WriteFile(file, newImageUrl, domainName, path);

                var filePath = newImageUrl.Replace(domainName, path).Split(',');

            }

            //return 0;
        }

        /// <summary>
        /// حذف فایل
        /// </summary>
        public static void DeleteFile(string oldImg, string domainName, string path)
        {
            if (!string.IsNullOrEmpty(oldImg))
            {
                var deleteImagePath = oldImg.Replace(domainName, path);

                if (System.IO.File.Exists(deleteImagePath))
                {
                    System.IO.File.Delete(deleteImagePath);
                }
            }
        }
        
        /// <summary>
        /// درج فایل
        /// </summary>
        public static void WriteFile(string file, string newImageUrl, string domainName, string path)
        {

            var filePath = newImageUrl.Replace(domainName, path);

            if (! string.IsNullOrEmpty(file))
            {
                var flag = true;
                //var myImage = image.Replace(" ", "+");
                byte[] bytes = Convert.FromBase64String(file); //myImage

                if (bytes.Length > 8700000) // 8 mb
                {
                    throw new Exception(ResourceMessage.Message.InvalidFileSize);
                }

                //foreach (var fileHead in allowedFiles)
                //{
                //    flag = !fileHead.Where((t, i) => t != bytes[i]).Any();

                //    if (flag)
                //    {
                //        break;
                //    }
                //}

                //if (!flag)
                //{
                //    throw new Exception(ResourceMessage.Message.InvalidFileType);
                //}

                System.IO.File.WriteAllBytes(filePath, bytes);
            }

        }

    }
}
