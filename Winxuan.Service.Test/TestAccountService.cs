using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winxuan.Data;
using Test.Data;
using Winxuan.Data.Model;
using Winxuan.Service.Interfaces;
using Winxuan.Service.Impl;
using Newtonsoft.Json;
using Winxuan.Infrastructure.DTO;
using System.Threading.Tasks;
using Winxuan.Infrastructure;
using System.Threading;
using Winxuan.Server;

namespace Winxuan.Service.Test
{
    [TestClass]
    public class TestAccountService:TestBase
    {
        private IAccountService service;

        public TestAccountService()
        {
            service = new AccountService(MockContext.Object);
        }

        /// <summary>
        ///  Login.Fail. No all data.
        /// </summary>
        [TestMethod]
        public void TestLogin_Fail_NoData()
        {
            //null login information.
            var result = service.Login(new Winxuan.Infrastructure.DTO.LoginDTO {});
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// Login. Fail. No token & timestamp information.
        /// </summary>
        [TestMethod]
        public void TestLogin_Fail_NoTokenTimestamp()
        {
            var result = service.Login(new LoginDTO { UserName = "admin", Password = "123" });
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// Login. Fail. Token & timestamp is not match.
        /// </summary>
        [TestMethod]
        public void TestLogin_Fail_TokenNotMatchTimeStamp()
        {
            string timeStamp = Convert.ToString(Utils.TimeStamp());
            Thread.Sleep(1000);
            var result = service.Login(new LoginDTO { UserName = "admin", Password = "123", 
                Token = Utils.LoginToken("admin", timeStamp), TimeStamp = Convert.ToString(Utils.TimeStamp()) });
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// Login.Success. By using all login information.
        /// </summary>
        [TestMethod]
        public void TestLogin_Success_AllData()
        {
            var timeStamp = Convert.ToString(Utils.TimeStamp());
            var result = service.Login(new LoginDTO { UserName = "admin", Password = "123", Token = Utils.LoginToken("admin", timeStamp), TimeStamp = timeStamp });
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject<UserLoginCache>(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// Login. success. By using Authtoken.
        /// </summary>
        [TestMethod]
        public void TestLogin_Success_AuthToken()
        {
            var timeStamp = Convert.ToString(Utils.TimeStamp());
            var result = service.Login(new LoginDTO { UserName = "admin", Password = "123", Token = Utils.LoginToken("admin", timeStamp), TimeStamp = timeStamp });
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject<LoginUserInfo>(result.Result);

            result = service.Login(new LoginDTO { AuthToken = response.Data.AuthToken });
            Task.WaitAll(result);
            var newResponse = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(newResponse.Status);
        }

        /// <summary>
        /// Login.Fail.By error AuthToken.
        /// </summary>
        [TestMethod]
        public void TestLogin_Fail_ErrorAuthToken()
        {
            var result = service.Login(new LoginDTO { AuthToken = "123" });
            Task.WaitAll(result);
            var newResponse = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(newResponse.Status);
        }

        /// <summary>
        /// Login.Fail.By AuthToken is out time.
        /// </summary>
        [TestMethod]
        public void TestLogin_Fail_AuthTokenOutTime()
        {
            var timeStamp = Convert.ToString(Utils.TimeStamp());
            var result = service.Login(new LoginDTO { UserName = "admin", Password = "123", Token = Utils.LoginToken("admin", timeStamp), TimeStamp = timeStamp });
            Task.WaitAll(result);
            var response = Winxuan.Infrastructure.WebUtils.DeserializeObject<LoginUserInfo>(result.Result);

            UserLoginCache.FindUser(response.Data.AuthToken).CacheDay = DateTime.Now.AddDays(-1000);

            result = service.Login(new LoginDTO { AuthToken = response.Data.AuthToken });
            Task.WaitAll(result);
            var newResponse = Winxuan.Infrastructure.WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(newResponse.Status);
        }
    }
}
