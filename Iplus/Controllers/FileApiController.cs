using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MyModels.DAL;
using MyModels.Entity;
using Iplus.Filters;
using System.Net.Http;
using System.Web.Security;
using MyServices.Base;

namespace Iplus.Controllers
{
    //[Authorize(Users = "Alice,Bob")]
    //[Authorize(Roles = "Administrators")]
    [Authorize]
    public class FileApiController : BaseApiController
    {

        public FileApiController()//IPostService iPostService
        {
            //_PostService = iPostService;
        }

        //private static readonly string ServerUploadFolder = "C:\\Temp"; //Path.GetTempPath();

        private string path = HttpContext.Current.Server.MapPath("~/Content/Files/");
        private string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Content/Files/";

        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<IHttpActionResult> UploadSingleFile()
        {

            try
            {
                //Request.Content.ReadAsMultipartAsync();

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["fileKey"];
                    var typeId = int.Parse(HttpContext.Current.Request.Form["typeId"]);

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        var fileName = Path.GetFileNameWithoutExtension(httpPostedFile.FileName);
                        var fileExtension = Path.GetExtension(httpPostedFile.FileName);
                        var postfix = Guid.NewGuid().ToString().Substring(0, 12);
                        fileName = fileName + "_" + postfix + fileExtension;

                        switch (typeId)
                        {
                            case (int)FileFormEnum.SpeakerImage:
                                path += "SpeakerImage/";
                                domainName += ("SpeakerImage/"+ fileName);
                                break;

                            case (int)FileFormEnum.SpeakerResume:
                                path += "SpeakerResume/";
                                domainName += ("SpeakerResume/"+ fileName);
                                break;

                            case (int)FileFormEnum.ConstImage:
                                path += "ConstImage/";
                                domainName += ("ConstImage/" + fileName);
                                break;

                            case (int)FileFormEnum.PostImage:
                                path += "PostImage/";
                                domainName += ("PostImage/" + fileName);
                                break;

                            case (int)FileFormEnum.UserImage:
                                path += "UserImage/";
                                domainName += ("UserImage/" + fileName);
                                break;
                        }
                        // Get the complete file path
                        var fileSavePath = Path.Combine(path, fileName);

                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);

                        var file = new TblFile()
                        {
                            FileName = fileName,
                            Extension = fileExtension,
                            FilePath = fileSavePath,
                            FileUrl = domainName,
                            FileLength = httpPostedFile.ContentLength,
                            MimeType = httpPostedFile.ContentType,//streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).FirstOrDefault(),
                            //Description = streamProvider.FormData["Description"],
                            ApprovedDate = utility.Date.GetDateTime.CurrentTimeSeconds(),
                            UserId = loginUserId,
                            SSTypeId = typeId
                            //DownloadLink = "TODO, will implement when file is persisited"
                        };

                        unitOfWork.FileRepository.Insert(file, loginUserId);

                        unitOfWork.Save();

                        return Ok(file);

                    }
                }



                //var streamProvider = new MultipartFormDataStreamProvider(path);

                //var fileName = streamProvider.FileData
                //    .Select(entry => entry.Headers.ContentDisposition.FileName)
                //    .FirstOrDefault();

                //var guid = Guid.NewGuid().ToString().Substring(0, 13);
                //var newImageUrl = (domainName + fileName + "_" + guid);

                //var file = new TblFile()
                //{
                //    FileName = streamProvider.FileData.Select(entry => entry.LocalFileName).FirstOrDefault(),
                //    FilePath = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName).FirstOrDefault(),
                //    MimeType = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType).FirstOrDefault(),
                //    Description = streamProvider.FormData["Description"],
                //    ApprovedDate = utility.Date.GetDateTime.CurrentTimeSeconds(),
                //    UserId = 1,
                //    //DownloadLink = "TODO, will implement when file is persisited"
                //};

                return BadRequest("خطا در ثبت فایل");

            }
            catch (Exception e)
            {
                return BadRequest("در عملیات ثبت خطایی رخ داده است");
            }

        }

        public IHttpActionResult Delete(int id)
        {
            //var temp = unitOfWork.FileRepository.GetById(id);
            //if (temp == null)
            //{
            //    return NotFound();
            //}

            //if (!string.IsNullOrEmpty(temp.FilePath))
            //{

            //    if (System.IO.File.Exists(temp.FilePath))
            //    {
            //        System.IO.File.Delete(temp.FilePath);
            //    }
            //}

            try
            {
                unitOfWork.FileRepository.Delete(id, loginUserId);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                return BadRequest("در حذف فایل خطایی رخ داده است");
            }
            
            return Ok();
        }

        //[Route("filesNoContentType")]
        //[HttpPost]
        //[ValidateMimeMultipartContentFilter]
        //public async Task<FileResult> UploadMultipleFiles2()
        //{
        //    var provider = new MultipartFormDataStreamProvider(ServerUploadFolder);
        //    try
        //    {
        //        var streamProvider = StreamConversion();
        //        await streamProvider.ReadAsMultipartAsync(provider);
        //        return new FileResult
        //        {
        //            FileNames = provider.FileData.Select(entry => entry.LocalFileName),
        //            Names = provider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
        //            Description = provider.FormData["description"],
        //            CreatedTimestamp = DateTime.UtcNow,
        //            UpdatedTimestamp = DateTime.UtcNow,
        //            DownloadLink = "TODO, will implement when file is persisited"
        //        };
        //    }
        //    catch (System.Exception e)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //    }
        //}

        private StreamContent StreamConversion()
        {
            Stream reqStream = Request.Content.ReadAsStreamAsync().Result;
            var tempStream = new MemoryStream();
            reqStream.CopyTo(tempStream);

            tempStream.Seek(0, SeekOrigin.End);
            var writer = new StreamWriter(tempStream);
            writer.WriteLine();
            writer.Flush();
            tempStream.Position = 0;

            var streamContent = new StreamContent(tempStream);
            foreach (var header in Request.Content.Headers)
            {
                streamContent.Headers.Add(header.Key, header.Value);
            }
            return streamContent;
        }

    }
}