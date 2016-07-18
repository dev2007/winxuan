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
    public class TestTeamService : TestBase
    {
        private ITeamService service;

        public TestTeamService()
        {
            service = new TeamService(MockContext.Object);
        }

        /// <summary>
        /// GetTeam.Success.Correct team id.
        /// </summary>
        [TestMethod]
        public void TestGetTeam_Success()
        {
            var result = service.GetTeam(1);
            Task.WaitAll(result);
            var response = WebUtils.DeserializeObject<TeamDTO>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Id, 1);
        }

        /// <summary>
        /// GetTeam. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestGetTeam_Fail_ErrorTeamId()
        {
            var result = service.GetTeam(-1);
            var response = WebUtils.DeserializeObject<TeamDTO>(result.Result);
            Assert.IsFalse(response.Status);
            Assert.IsNull(response.Data);
        }

        /// <summary>
        /// DeleteTeam. Success.
        /// </summary>
        [TestMethod]
        public void TestDeleteTeam_Success()
        {
            var result = service.DeleteTeam(1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        ///  DeleteTeam. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestDeleteTeam_Fail_ErrorTeamId()
        {
            var result = service.DeleteTeam(-1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// UpdateTeam. Success.
        /// </summary>
        [TestMethod]
        public void TestUpdateTeam_Success()
        {
            var desc = "new";
            var result = service.UpdateTeam(new TeamDTO() { Id = 1, TeamDescription = desc });
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);

            var teamResult = service.GetTeam(1);
            var teamResponse = WebUtils.DeserializeObject<TeamDTO>(teamResult.Result);

            Assert.AreEqual(teamResponse.Data.TeamDescription, desc);
        }

        /// <summary>
        /// UpdateTeam. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestUpdateTeam_Fail_ErrorTeamId()
        {
            var desc = "new";
            var result = service.UpdateTeam(new TeamDTO() { Id = -1, TeamDescription = desc });
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// CreateTeam. Success.
        /// </summary>
        [TestMethod]
        public void TestCreateTeam_Success()
        {
            var name = "create name";
            var desc = "create desc";
            var result = service.CreateTeam(new TeamDTO() { TeamName = name, TeamDescription = desc });
            var response = WebUtils.DeserializeObject<TeamDTO>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.TeamName, name);
            Assert.AreEqual(response.Data.TeamDescription, desc);
        }

        /// <summary>
        /// CreateTeam. Fail. Transfer null data.
        /// </summary>
        [TestMethod]
        public void TestCreateTeam_Fail_NullData()
        {
            var result = service.CreateTeam(null);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// GetTeams. Success. First page.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Success_FirstPage()
        {
            var result = service.GetTeams(1, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count(), 1);
            Assert.AreEqual(response.Data.ElementAt(0).Id, 1);
        }

        /// <summary>
        /// GetTeams. Success. Second page.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Success_SecondPage()
        {
            var result = service.GetTeams(2, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count(), 1);
            Assert.AreEqual(response.Data.ElementAt(0).Id, 2);
        }

        /// <summary>
        /// GetTeams. Fail. Error page:too big.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_BigPage()
        {
            var result = service.GetTeams(Int32.MaxValue, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count(), 0);
        }

        /// <summary>
        /// GetTeams. Fail. Error page: 0.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_ZeroPage()
        {
            var result = service.GetTeams(0, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// GetTeams. Fail. Error page:negative.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_NegativePage()
        {
            var result = service.GetTeams(-1, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// GetTeams. Fail. Error page size: 0.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_ZeroPageSize()
        {
            var result = service.GetTeams(1, 0);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// GetTeams. Fail. Error page size:negative.
        /// </summary>
        [TestMethod]
        public void TestGetTeams_Fail_NegativePageSize()
        {
            var result = service.GetTeams(1, -1);
            var response = WebUtils.DeserializeObject<IEnumerable<TeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

    }
}
