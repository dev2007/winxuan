using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            result.Status = isSuccess;
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
        public static string Json(object data = null)
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
        /// Create json for failure.The default data is string.Empty.
        /// </summary>
        /// <param name="errorMsg">the message of failure.</param>
        /// <returns></returns>
        public static string Json(string errorMsg)
        {
            return ResponseFail.Json("", errorMsg);
        }

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
            return ResponseInfo.Json(false, data, errorMsg, 204);
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

        /// <summary>
        /// Fail for Unauthorized 401.
        /// 非授权操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Forbidden(object data = null)
        {
            return ResponseFail.Json(data, "非授权操作", 401);
        }

        /// <summary>
        /// Fail for NoContent 204.
        /// 无相应数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string NoContent(object data = null)
        {
            return ResponseFail.Json(data, "无可操作数据", 204);
        }

        /// <summary>
        /// Fail for ExpectationFailed 417.
        /// 发生异常
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ExpectationFailed(object data = null, string message = "")
        {
            return ResponseFail.Json(data, message, 417);
        }
    }
}
