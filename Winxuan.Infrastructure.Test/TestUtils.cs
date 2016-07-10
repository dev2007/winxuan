using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Winxuan.Infrastructure.Test
{
    [TestClass]
    public class TestUtils
    {
        [TestMethod]
        public void TestBiggerNow()
        {
            long timeStamp = Utils.TimeStamp();
            Assert.IsFalse(Utils.BiggerNow(timeStamp));
        }

        [TestMethod]
        public void TestCompareMD5()
        {
            string str = "test-str";
            string sourceMD5 = Utils.MD5(str);
            Assert.IsTrue(Utils.CompareMD5(sourceMD5, str));
        }
    }
}
