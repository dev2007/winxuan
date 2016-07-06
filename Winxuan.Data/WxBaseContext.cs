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
    }
}
