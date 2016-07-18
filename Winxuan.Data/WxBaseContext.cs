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

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<TeamWithUser> TeamWithUsers { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}
