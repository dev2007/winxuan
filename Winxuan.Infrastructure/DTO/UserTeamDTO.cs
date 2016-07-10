using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure.DTO
{
    public class UserTeamDTO
    {
        public int UserId { get; set; }

        public int TeamId { get; set; }

        public string TeamDescription { get; set; }

        public int RoleId { get; set; }

        public string RoleDescription { get; set; }
    }
}
