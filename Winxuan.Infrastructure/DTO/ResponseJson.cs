using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure.DTO
{
    public class ResponseJson : ResponseJson<object>
    {
    }

    public class ResponseJson<T>
    {
        /// <summary>
        /// Result flag,true or false.It's same to the Result.
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// Result message,success or fail.
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Response data.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Error message when Result is fail.
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// Status code.
        /// </summary>
        public int StatusCode { get; set; }
    }
}
