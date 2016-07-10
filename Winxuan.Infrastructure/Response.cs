using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Infrastructure
{
    /// <summary>
    /// Json Response base class.
    /// </summary>
    public class ResponseInfo
    {
        /// <summary>
        /// message success flag.
        /// </summary>
        protected const string Succcess = "Success";
        /// <summary>
        /// message failure flag.
        /// </summary>
        protected const string Fail = "Fail";

        /// <summary>
        /// create response json information.
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="data"></param>
        /// <param name="errorMsg"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static string Json(bool isSuccess, object data, string errorMsg = "", int statusCode = 200)
        {
            ResponseJson result = new ResponseJson();
            result.State = isSuccess;
            result.Result = isSuccess ? Succcess : Fail;
            result.Data = data;
            result.ErrorMsg = errorMsg;
            result.StatusCode = statusCode;
            return JsonConvert.SerializeObject(result);
        }
    }

    /// <summary>
    /// Json response for success. 
    /// </summary>
    public class ResponseSuccess : ResponseInfo
    {
        /// <summary>
        /// create json response.
        /// </summary>
        /// <param name="data">the data of response.</param>
        /// <returns></returns>
        public static string Json(object data=null)
        {
            return Json(true, data);
        }
    }

    /// <summary>
    /// Json response for failure.
    /// </summary>
    public class ResponseFail : ResponseInfo
    {
        /// <summary>
        /// create json for failure.The default status code is 204.
        /// </summary>
        /// <param name="data">the data of response.</param>
        /// <param name="errorMsg">the message of failure.Must set the value or the function will throw an exception.</param>
        /// <returns></returns>
        public static string Json(object data, string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
                throw new Exception("Not set error message");
            return ResponseInfo.Json(false, data, errorMsg,204);
        }

        /// <summary>
        /// create json for failure.
        /// </summary>
        /// <param name="data">the data of response.</param>
        /// <param name="errorMsg">the message of failure.Must set the value or the function will throw an exception.</param>
        /// <param name="statusCode">status code</param>
        /// <returns></returns>
        public static string Json(object data, string errorMsg, int statusCode)
        {
            if (string.IsNullOrEmpty(errorMsg))
                throw new Exception("Not set error message");
            return ResponseInfo.Json(false, data, errorMsg, statusCode);
        }
    }
}
