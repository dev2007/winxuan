using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Winxuan.File;

namespace Winxuan.WebApi.Controllers
{
    public class FileController : ApiController
    {
        /// <summary>
        /// File Uplload.
        /// </summary>
        /// <returns></returns>
        public async Task<string> Post()
        {
            return await FileStorage.UploadFile(Request);
        }
    }
}
