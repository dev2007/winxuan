using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Winxuan.Infrastructure.Test
{
    [TestClass]
    public class TestLoginUserInfo
    {

        [TestMethod]
        public void TestOutTime()
        {
            LoginUserInfo info = new LoginUserInfo();
            Assert.IsFalse(info.OutTime());
        }
    }
}
