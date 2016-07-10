using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Data.Model
{
    public class TeamWithUser
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int UserId { get; set; }

        public int UserRole { get; set; }
    }
}
