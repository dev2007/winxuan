using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winxuan.Infrastructure.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserRole { get; set; }

        public string RoleDescription { get; set; }
    }
}
