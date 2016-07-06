using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Infrastructure.DTO;

namespace Winxuan.Service.Interfaces
{
    public interface IAccountService
    {
        Task<string> Registe(RegisteDTO register);
        Task<string> Login(LoginDTO login);
        Task<string> Logout(string userName);
    }
}
