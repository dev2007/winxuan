using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Impl;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Test
{
    [TestClass]
    public class TestUserTeamService:TestBase
    {
        private IUserTeamService service;

        public TestUserTeamService()
        {
            this.service = new UserTeamService(MockContext.Object);
        }

        /// <summary>
        /// GetTeams. Success.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Success()
        {
            var result = service.GetTeams(2);
            var response = WebUtils.DeserializeObject<IList<UserTeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count, 2);
        }

        /// <summary>
        /// GetTeams. Fail. Error user id. 
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_ErrorUserId()
        {
            var result = service.GetTeams(-1);
            var response = WebUtils.DeserializeObject<IList<UserTeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count, 0);
        }
    }
}
