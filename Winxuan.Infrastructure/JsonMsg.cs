using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Winxuan.Infrastructure
{
    public class JsonMsg
    {
        public bool Status { get; set; }

        public int StatusCode { get; set; }

        public String Msg { get; set; }

        public object Data { get; set; }
    }
}