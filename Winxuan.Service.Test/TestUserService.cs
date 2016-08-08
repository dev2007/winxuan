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
    public class TestUserService : TestBase
    {
        private IUserService service;

        public TestUserService()
        {
            this.service = new UserService(MockContext.Object);
        }

        /// <summary>
        /// GetUser. Success.
        /// </summary>
        [TestMethod]
        public void TestGetUser_Success()
        {
            var result = service.GetUser(1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// GetUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestGetUser_Fail_ErrorUserId()
        {
            var result = service.GetUser(-1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// CreateUser. Success.
        /// </summary>
        [TestMethod]
        public void TestCreateUser_Success()
        {
            var result = service.CreateUser(new RegisteDTO()
            {
                UserName = "new user name",
                Name = "new name",
                Password = "password",
                RePassword = "password"
            });
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// CreateUser. Fail. All fail conditions.
        /// </summary>
        [TestMethod]
        public void TestCreateUser_Fail()
        {
            //1. null data.
            var result = service.CreateUser(null);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //2.empty username.
            result = service.CreateUser(new RegisteDTO()
                {

                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //3.empty name.
            result = service.CreateUser(new RegisteDTO()
                {
                    UserName = "new user name"
                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //4.empty password.
            result = service.CreateUser(new RegisteDTO()
                {
                   UserName = "new user name",
                   Name = "new name"
                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //5.repassword is empty(not equals to password same).
            result = service.CreateUser(new RegisteDTO()
                {
                    UserName = "new user name",
                    Name = "new name",
                    Password = "password"
                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //6. repassword is not empty,but not equals to password.
            result = service.CreateUser(new RegisteDTO()
                {
                    UserName = "new user name",
                    Name = "new name",
                    Password = "password",
                    RePassword = "not password"
                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);

            //7. username is equals to other user's username.
            result = service.CreateUser(new RegisteDTO()
                {
                    UserName = "admin",
                    Name = "new name",
                    Password = "password",
                    RePassword = "password"
                });
            response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }


        /// <summary>
        /// UpdateUser. Success.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var result = service.UpdateUser(new UserDTO()
            {
                    Id = 1,
                    Name = "new test name"
            });

            var response = WebUtils.DeserializeObject(result.Result);

            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// UpdateUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Fail_ErrorUserId()
        {
            var result = service.UpdateUser(new UserDTO()
            {
               Id = -1,
               Name = "new test name"
            });

            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        public void TestUpdateUser_Fail_EmptyName()
        {
            var result = service.UpdateUser(new UserDTO()
            {
               Id = 1,
               Name = ""
            });

            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }

        /// <summary>
        /// DeleteUser. Success.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var result = service.DeleteUser(1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsTrue(response.Status);
        }

        /// <summary>
        /// DeleteUser. Fail. Error user id.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Fail_ErrorUserId()
        {
            var result = service.DeleteUser(-1);
            var response = WebUtils.DeserializeObject(result.Result);
            Assert.IsFalse(response.Status);
        }
    }
}
