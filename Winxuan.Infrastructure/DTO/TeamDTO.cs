using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public string TeamDescription { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }
    }
}
