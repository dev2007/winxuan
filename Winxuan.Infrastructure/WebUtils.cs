using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Infrastructure
{
    /// <summary>
    /// Http helper class.
    /// </summary>
    public class WebUtils
    {
        /// <summary>
        /// Http Get.
        /// </summary>
        /// <typeparam name="T">T is data type in ResponseJson.Data.</typeparam>
        /// <param name="url">api url.</param>
        /// <param name="authToken">token after login success.</param>
        /// <returns></returns>
        public static ResponseJson<T> Get<T>(string url, string authToken) where T : class
        {
            string result = BaseReq(url, HttpMethod.GET,authToken:authToken);
            return WebUtils.DeserializeObject<T>(ProcessResponseJson(result));
        }

        /// <summary>
        /// Http Post.
        /// </summary>
        /// <typeparam name="T">T is data type in ResponseJson.Data.</typeparam>
        /// <param name="url">api url.</param>
        /// <param name="data">data to transfer.</param>
        /// <param name="authToken">token after login success.</param>
        /// <returns></returns>
        public static ResponseJson<T> Post<T>(string url, object data,string authToken="") where T : class
        {
            string jsonParams = JsonConvert.SerializeObject(data);
            string result = BaseReq(url, HttpMethod.POST, jsonParams,authToken);
            return WebUtils.DeserializeObject<T>(ProcessResponseJson(result));
        }

        private static string ProcessResponseJson(string result)
        {
            result = result.Substring(1);
            result = result.Substring(0, result.Length - 1).Replace(@"\", "");
            return result;
        }

        /// <summary>
        /// Base request function. for http request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="jsonParams">method params.type is json.</param>
        /// <param name="authToken">token after login success.</param>
        /// <returns></returns>
        private static string BaseReq(string url, HttpMethod method, string jsonParams = "",string authToken = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = method == HttpMethod.GET ? "GET" : "POST";
            request.Timeout = 10000;
            SetAuthToken(request,authToken);
            if (method == HttpMethod.POST)
            {
                //request.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                byte[] bytes = Encoding.UTF8.GetBytes(jsonParams);
                request.ContentLength = bytes.Length;
                request.ContentType = "text/json";

                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);
                reqstream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd().ToString();
            response.GetResponseStream().Close();
            return result;
        }

        /// <summary>
        /// Set the authorize token in http request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="authToken"></param>
        public static void SetAuthToken(HttpWebRequest request,string authToken)
        {
            request.Headers.Add("AuthToken", authToken);
        }

        /// <summary>
        /// Deserialize response Json to ResponseJson object.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResponseJson DeserializeObject(string result)
        {
            return JsonConvert.DeserializeObject<ResponseJson>(result);
        }

        /// <summary>
        /// Deserialize response Json to ResponseJson object.{T} is the ResposeJson.Data type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ResponseJson<T> DeserializeObject<T>(string result)
        {
            return JsonConvert.DeserializeObject<ResponseJson<T>>(result);
        }
    }


    /// <summary>
    /// Http method type.
    /// </summary>
    enum HttpMethod
    {
        GET = 1,
        POST = 2
    }
}
