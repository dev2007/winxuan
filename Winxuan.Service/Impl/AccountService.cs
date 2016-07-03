using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;
using Winxuan.Infrastructure.DTO;
using Winxuan.Service.Interfaces;

namespace Winxuan.Service.Impl
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(WxContext context)
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
                else if (string.IsNullOrEmpty(login.UserName))
                    return ResponseFail.Json("", "用户名为空");
                else if (string.IsNullOrEmpty(login.Password))
                    return ResponseFail.Json("", "密码为空");
                User user = context.Users.ToList().First(t => t.UserName == login.UserName);
                if (user == null)
                {
                    return CheckLoginInfo();
                }
                UserSecurity us = context.UserSecurities.ToList().First(t => t.UserId == user.ID);
                if (us == null)
                {
                    return ResponseFail.Json("", "未设置密码");
                }
                else
                {
                    if (us.Password == login.Password)
                        return ResponseSuccess.Json("");
                    else
                        return CheckLoginInfo();
                }
            });
        }

        private string CheckLoginInfo()
        {
            return ResponseFail.Json("", "用户名或密码错误");
        }
    }
}
