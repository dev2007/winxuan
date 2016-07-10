using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Winxuan.Web.Models
{
    public class TeamViewModel
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }

        public string TeamDescription { get; set; }

        public int RoleId { get; set; }

        public string RoleDescription { get; set; }
    }
}