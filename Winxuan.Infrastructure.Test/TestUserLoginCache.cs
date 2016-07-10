using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winxuan.Data.Model;

namespace Winxuan.Infrastructure.Test
{
    [TestClass]
    public class TestUserLoginCache
    {
        string tokenKey = "12345";

        [TestMethod]
        public void TestAddUserCache()
        {
            Assert.IsTrue(AddUserCache());
        }

        private bool AddUserCache()
        {
            bool result = UserLoginCache.AddUserCache(tokenKey, new User() { Id = 1, Name = "testname", Password = "testpwd", UserName = "testusername" });
            return result;
        }

        [TestMethod]
        public void TestContainsKey()
        {
            AddUserCache();
            Assert.IsTrue(UserLoginCache.ContainsKey(tokenKey));
            Assert.IsFalse(UserLoginCache.ContainsKey("&&&&&"));
        }

        [TestMethod]
        public void TestIsLogin()
        {
            AddUserCache();
            Assert.IsTrue(UserLoginCache.IsLogin(tokenKey));
        }

        [TestMethod]
        public void TestRemoveCache()
        {
            AddUserCache();
            Assert.IsTrue(UserLoginCache.RemoveCache(tokenKey));
            Assert.IsTrue(UserLoginCache.RemoveCache("&&&&&"));
        }

        [TestMethod]
        public void TestFindUser()
        {
            AddUserCache();
            LoginUserInfo info = UserLoginCache.FindUser(tokenKey);
            Assert.IsNotNull(info);
            info = UserLoginCache.FindUser("&&&&&");
            Assert.IsNotNull(info);

            bool exception = false;
            try
            {
                UserLoginCache.FindUser("");
            }
            catch
            {
                exception = true;
            }

            Assert.IsTrue(exception);
        }
    }
}
