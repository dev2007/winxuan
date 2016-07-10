using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data.Model;

namespace Winxuan.Data
{
    public class WxBaseContext : DbContext
    {
        public WxBaseContext(string nameOrConnectstr)
            : base(nameOrConnectstr)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamWithUser> TeamWithUsers { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
