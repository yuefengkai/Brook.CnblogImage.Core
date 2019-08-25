using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using MetaWeblogClient;
using Polly;

namespace Brook.CnblogImage.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public static Client BlogClient;
        public ImageController()
        {
             BlogClient = new Client(BlogInfoHelper.GetBlogInfo());
        }

        // POST api/values
        [HttpPost("Upload")]
        public IActionResult Post()
        {
            string token = Request.Headers["Authorization"];

            if (token != Environment.GetEnvironmentVariable("ASPNETCORE_TOKEN"))
            {
                return StatusCode(401, new { result = 0, msg = "Token is Error,Pllease Check Environment ASPNETCORE_TOKEN" });
            }

            if (Request.Form.Files.Count == 0)
            {
                return StatusCode(400, new { result = 0, msg = "上传文件不能为空" });
            }

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Images");

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToFileTime() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

#if DEBUG
                    var cnblogImageUrl = dbPath; //UploadFileToCnblog(dbPath);
#elif RELEASE
                    var cnblogImageUrl = UploadFileToCnblog(dbPath);
#endif

                    Console.WriteLine($"图片上传成功，{cnblogImageUrl}");


                    return Ok(new { cnblogImageUrl = cnblogImageUrl });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = 0, msg = ex.Message});
            }
        }

        private string UploadFileToCnblog(string filePath)
        {
           

            var policy = Policy.Handle<Exception>().Retry(3, (exception, retryCount) =>
            {
                Console.WriteLine("上传失败，正在重试 {0}，异常：{1}", retryCount, exception.Message);
            });
            try
            {
                var url = policy.Execute<string>(() =>
                {
                    FileInfo fileinfo = new FileInfo(filePath);
                    var mediaObjectInfo = BlogClient.NewMediaObject(fileinfo.Name, "image/jpeg", System.IO.File.ReadAllBytes(filePath));
                    return mediaObjectInfo.URL;
                });

                return url;
            }
            catch (Exception e)
            {
                Console.WriteLine("上传失败,异常：{0}", e.Message);
                throw;
            }
        }
    }
}
