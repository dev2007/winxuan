using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(WxBaseContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
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
        /// Logout.
        /// </summary>
        /// <param name="authToken"></param>
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

        private string CheckLoginInfo()
        {
            return ResponseFail.Json("", "用户名或密码错误");
        }

        /// <summary>
        /// Registe.
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public Task<string> Registe(RegisteDTO register)
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
                else if (context.Users.ToList().Where(t=> t.UserName == register.UserName).Count() > 0)
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
    }
}
