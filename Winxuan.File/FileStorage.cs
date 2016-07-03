using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Winxuan.File
{
    /// <summary>
    /// File storage.
    /// </summary>
    public class FileStorage
    {
        //save file path.
        private static readonly string Folder = @"D:\upload";

        /// <summary>
        /// Upload File.
        /// </summary>
        /// <param name="request">request object.</param>
        /// <returns></returns>
        public static Task<string> UploadFile(HttpRequestMessage request)
        {
            if (request.Content.IsMimeMultipartContent())
            {
                Stream reqStream = request.Content.ReadAsStreamAsync().Result;
                if (reqStream.CanSeek)
                {
                    reqStream.Position = 0;
                }

                long teamId = 0;
                string path = string.Format(@"{0}\{1}", Folder, teamId);
                //var streamProvider = new MultipartFormDataStreamProvider(path);
                var streamProvider = new ReNameMultipartFormDataStreamProvider(path);

                var task = request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                        request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);

                    var fileInfo = streamProvider.FileData[0];
                    var info = new FileInfo(fileInfo.LocalFileName);
                    return info.FullName;

                });
                return task;
            }
            else
            {
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
            }
        }
    }

    public class ReNameMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public ReNameMultipartFormDataStreamProvider(string root) : base(root) { }
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {            //截取文件扩展名           
            string exp = Path.GetExtension(headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"'));
            string name = base.GetLocalFileName(headers);
            return name + exp;
        }
    }
}
