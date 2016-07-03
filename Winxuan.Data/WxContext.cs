using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Data
{
    public class WxContext : DbContext
    {
        public WxContext()
            : base("WxContext")
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<UserSecurity> UserSecurities { get; set; }
    }
}
