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
    public class TestTeamUserService : TestBase
    {
        private ITeamUserService service;

        public TestTeamUserService()
        {
            service = new TeamUserService(MockContext.Object);
        }


        /// <summary>
        /// GetUsers. Success.
        /// </summary>
        [TestMethod]
        public void TestGetUsers_Success()
        {
            var result = service.GetUsers(1);
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);
            Assert.AreEqual(response.Data.Count(), 2);
        }

        /// <summary>
        /// GetUsers. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestGetUsers_Fail_ErrorTeamId()
        {
            var result = service.GetUsers(-1);
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// AddUser. Success.
        /// </summary>
        [TestMethod]
        public void TestAddUser_Success()
        {
            var result = service.AddUser(3, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);

            var newResult = service.GetUsers(1);
            var newResponse = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(newResult.Result);
            Assert.IsTrue(newResponse.Status);
            Assert.AreEqual(newResponse.Data.Count(), 3);
        }

        /// <summary>
        /// AddUser. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestAddUser_Fail_ErrorTeamid()
        {
            var result = service.AddUser(3, -1);
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// AddUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestAddUser_Fail_ErrorUserid()
        {
            var result = service.AddUser(-1, 1);
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// UpdateUser. Success.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var result = service.UpdateUser(new UserTeamDTO() { TeamId = 1, UserId = 2, RoleId = 3 });
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsTrue(response.Status);

            var newResult = service.GetUsers(1);
            var newRespose = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(newResult.Result);
            var teamUser = newRespose.Data.First(t => t.UserId == 2);
            Assert.AreEqual(teamUser.RoleId, 3);
        }

        /// <summary>
        /// UpdateUser. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Fail_ErrorTeamId()
        {
            var result = service.UpdateUser(new UserTeamDTO() { TeamId = -1, UserId = 2, RoleId = 3 });
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// UpdateUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Fail_ErrorUserId()
        {
            var result = service.UpdateUser(new UserTeamDTO() { TeamId = 1, UserId = -1, RoleId = 3 });
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// UpdateUser. Fail. Error role id.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Fail_ErrorRoleId()
        {
            var result = service.UpdateUser(new UserTeamDTO() { TeamId = 1, UserId = 1, RoleId = -1 });
            var response = WebUtils.DeserializeObject<IEnumerable<UserTeamDTO>>(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// DeleteUser. Success.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var result = service.DeleteUser(1, 1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// DeleteUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Fail_ErrorUserId()
        {
            var result = service.DeleteUser(-1, 1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// DeleteUser. Fail. Error team id.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Fail_ErrorTeamId()
        {
            var result = service.DeleteUser(1, -1);
            var resposne = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(resposne.Status);
        }

    }
}
