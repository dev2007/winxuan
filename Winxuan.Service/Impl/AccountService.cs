using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Server;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    /// <summary>
    /// Account service class.
    /// </summary>
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="dto">Login information.</param>
        /// <returns></returns>
        public Task<string> Login(LoginDTO login)
        {
            return Task.Run(() =>
            {
                if (login == null)
                {
                    return ResponseFail.Json("", "用户名和密码为空");
                }
                else if (!string.IsNullOrEmpty(login.AuthToken))
                {
                    LoginUserInfo userInfo = UserLoginCache.FindUser(login.AuthToken);
                    if(userInfo.ID == 0)
                    {
                        return ResponseFail.Json("[AuthToken]无效");
                    }

                    if (!userInfo.OutTime())
                    {
                        return ResponseSuccess.Json(UserLoginCache.FindUser(login.AuthToken));
                    }
                }
                else if (string.IsNullOrEmpty(login.UserName))
                    return ResponseFail.Json("", "用户名为空");
                else if (string.IsNullOrEmpty(login.Password))
                    return ResponseFail.Json("", "密码为空");
                else if (string.IsNullOrEmpty(login.TimeStamp))
                    return ResponseFail.Json("", "参数异常，请检查[TimeStamp]");
                else if (!Utils.CompareMD5(login.Token, string.Format("{0}-{1}", login.UserName, login.TimeStamp)))
                    return ResponseFail.Json("", "参数异常，请检查[Token]");

                User user = context.Users.ToList().Find(t=> t.UserName == login.UserName);
                if (user == null)
                {
                    return CheckLoginInfo();
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    return ResponseFail.Json("", "未设置密码");
                }
                else
                {
                    if (user.Password == login.Password)
                    {
                        string token = Utils.MD5(string.Format("{0}-{1}-{2}", user.UserName, login.TimeStamp, DateTime.Now.ToUniversalTime().ToString()));
                        UserLoginCache.AddUserCache(token, user);
                        return ResponseSuccess.Json(UserLoginCache.FindUser(token));
                    }
                    else
                        return CheckLoginInfo();
                }
            });
        }

        /// <summary>
        /// User logout. 
        /// </summary>
        /// <param name="authToken">User's authorized token.</param>
        /// <returns></returns>
        public Task<string> Logout(string authToken)
        {
            return Task.Run(() =>
            {
                if (UserLoginCache.ContainsKey(authToken))
                {
                    bool result = UserLoginCache.RemoveCache(authToken);
                    return result ? ResponseSuccess.Json() : ResponseFail.Json("", "注销失败，请重试");
                }
                else
                    return ResponseSuccess.Json();
            });
        }

        /// <summary>
        /// Error message for login.
        /// </summary>
        /// <returns></returns>
        private string CheckLoginInfo()
        {
            return ResponseFail.Json("", "用户名或密码错误");
        }
    }
}
