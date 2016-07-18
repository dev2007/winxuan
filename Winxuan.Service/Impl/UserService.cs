using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    /// <summary>
    /// user service class.
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        public UserService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get user data by user's id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        public Task<string> GetUser(int userId)
        {
            return Task.Run(() =>
                {
                    var result = context.Users.Find(userId);
                    if (result == null)
                        return ResponseFail.Json("", "无此用户数据");

                    return ResponseSuccess.Json(result);
                });
        }

        /// <summary>
        /// Create a new user data.
        /// </summary>
        /// <param name="dto">New user's data.</param>
        /// <returns></returns>
        public Task<string> CreateUser(Infrastructure.DTO.RegisteDTO register)
        {
            return Task.Run(() =>
            {
                if (register == null)
                {
                    return ResponseFail.Json("", "注册信息未填写");
                }
                else if (string.IsNullOrEmpty(register.UserName))
                    return ResponseFail.Json("", "用户名未填写");
                else if (string.IsNullOrEmpty(register.Name))
                    return ResponseFail.Json("", "昵称/真实姓名未填写");
                else if (string.IsNullOrEmpty(register.Password))
                    return ResponseFail.Json("", "密码未填写");
                else if (register.Password != register.RePassword)
                    return ResponseFail.Json("", "两次密码填写不一致");
                else if (context.Users.ToList().Where(t => t.UserName == register.UserName).Count() > 0)
                {
                    return ResponseFail.Json("", "用户名重复，请换一个");
                }

                var user = new User()
                {
                    UserName = register.UserName,
                    Name = register.Name,
                    Password = register.Password
                };

                context.Users.Add(user);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return ResponseFail.Json("", e.Message);
                }

                return ResponseSuccess.Json("注册成功，请登录");
            });
        }

        /// <summary>
        /// Update user's data.
        /// </summary>
        /// <param name="dto">User's data.</param>
        /// <returns></returns>
        public Task<string> UpdateUser(Infrastructure.DTO.UserDTO dto)
        {
            return Task.Run(() =>
                {
                    var user = context.Users.Find(dto.Id);
                    if (user == null)
                    {
                        return ResponseFail.Json("", "无此用户，操作失败", 204);
                    }

                    user.Name = dto.Name;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.Json("", e.Message);
                    }

                    return ResponseSuccess.Json();
                });
        }

        /// <summary>
        /// Delete user by user id.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns></returns>
        public Task<string> DeleteUser(int userId)
        {
            return Task.Run(() =>
            {
                var user = context.Users.Find(userId);
                if (user == null)
                {
                    return ResponseFail.Json("", "无此用户，操作失败", 204);
                }

                context.Users.Remove(user);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return ResponseFail.Json("", e.Message);
                }

                return ResponseSuccess.Json();
            });
        }

        /// <summary>
        /// Get users by page index and page size.
        /// </summary>
        /// <param name="page">Query page index.First is 1.</param>
        /// <param name="pageSize">Data size per page.</param>
        /// <returns></returns>
        public Task<string> GetUsers(int page, int pageSize = 30)
        {
            return Task.Run(() =>
                {
                    try
                    {
                        var excludePre = context.Users.ToList().Take((page - 1) * pageSize);
                        var result = context.Users.ToList().Except(excludePre).Take(pageSize);

                        return ResponseSuccess.Json(result);
                    }
                    catch (Exception e)
                    {
                        return ResponseFail.ExpectationFailed(message: e.Message);
                    }
                });
        }
    }
}
