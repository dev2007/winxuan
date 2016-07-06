using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Infrastructure
{
    public class WebUtils
    {
        public static string Get(string url)
        {
            string result = BaseReq(url, HttpMethod.GET);
            return result;
        }

        public static ResponseJson<T> Post<T>(string url, object data) where T : class
        {
            string jsonParams = JsonConvert.SerializeObject(data);
            string result = BaseReq(url, HttpMethod.POST, jsonParams);
            result = result.Substring(1);
            result = result.Substring(0, result.Length - 1).Replace(@"\", "");
            return JsonConvert.DeserializeObject<ResponseJson<T>>(result);
        }

        private static string BaseReq(string url, HttpMethod method, string jsonParams = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = method == HttpMethod.GET ? "GET" : "POST";
            request.Timeout = 10000;
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
    }

    enum HttpMethod
    {
        GET = 1,
        POST = 2
    }
}
