using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Web.Controllers
{
    public class FileController : BaseController
    {
        private const string StorePath = @"D:\upload\";
        /// <summary>
        /// file upload.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult Upload(HttpPostedFileBase file,string teamId)
        {
            if (!System.IO.Directory.Exists(StorePath+teamId))
            {
                System.IO.Directory.CreateDirectory(StorePath+teamId);
            }
            file.SaveAs(Path.Combine(StorePath+teamId, file.FileName));
            return Json(new JsonMsg() { Status = true });
        }

    }
}
