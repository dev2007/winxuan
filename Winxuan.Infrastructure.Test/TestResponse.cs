using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Infrastructure.Test
{
    [TestClass]
    public class TestResponse
    {
        [TestMethod]
        public void TestResponseInfo_Json()
        {
            string errorMsg = "testErrorMsg";
            string json = ResponseInfo.Json(true, "123", errorMsg, 201);
            ResponseJson obj = JsonConvert.DeserializeObject<ResponseJson>(json);
            Assert.IsTrue(obj.Status);
            Assert.AreEqual(obj.ErrorMsg, errorMsg);

            json = ResponseInfo.Json(false, "123", errorMsg, 201);
            obj = JsonConvert.DeserializeObject<ResponseJson>(json);
            Assert.IsFalse(obj.Status);
            Assert.AreEqual(obj.ErrorMsg, errorMsg);

            json = ResponseInfo.Json(true, new string[]{"a1","a2","a3"}, errorMsg, 201);
            ResponseJson<string[]> objArray = JsonConvert.DeserializeObject<ResponseJson<string[]>>(json);
            Assert.IsTrue(objArray.Status);
            Assert.AreEqual(objArray.ErrorMsg, errorMsg);
            Assert.AreEqual(objArray.Data[0], "a1");
        }

        [TestMethod]
        public void TestResponseSuccess_Json()
        {
            string json = ResponseSuccess.Json();
            ResponseJson obj = JsonConvert.DeserializeObject<ResponseJson>(json);
            Assert.IsTrue(obj.Status);

            json = ResponseSuccess.Json("123");
            ResponseJson<string>  objStr = JsonConvert.DeserializeObject<ResponseJson<string>>(json);
            Assert.IsTrue(objStr.Status);
            Assert.AreEqual(objStr.Data, "123");
        }

        [TestMethod]
        public void TestResponseFail_Json()
        {
            string json = ResponseFail.Json("","testerrormsg");
            ResponseJson obj = JsonConvert.DeserializeObject<ResponseJson>(json);
            Assert.IsFalse(obj.Status);

            json = ResponseFail.Json("123", "testerrormsg", 401);
            ResponseJson<string> objStr = JsonConvert.DeserializeObject<ResponseJson<string>>(json);
            Assert.IsFalse(objStr.Status);
            Assert.AreEqual(objStr.Data, "123");
            Assert.AreEqual(objStr.StatusCode, 401);
        }
    }
}
