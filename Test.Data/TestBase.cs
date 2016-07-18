using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winxuan.Data;
using Winxuan.Data.Model;

namespace Test.Data
{
    public class TestBase
    {
        protected MockedDbContext<WxSLContext> MockContext = EntityFrameworkMockHelper.GetMockContext<WxSLContext>();

        public TestBase()
        {
            MockContext.Object.Users.Add(new User { Id = 1, UserName = "admin", Name = "admin", Password = "123" });
            MockContext.Object.Users.Add(new User { Id = 2, UserName = "user1", Name = "user1", Password = "123" });
            MockContext.Object.Users.Add(new User { Id = 3, UserName = "user2", Name = "user2", Password = "123" });

            MockContext.Object.UserRoles.Add(new UserRole { Id = 1, RoleDescription = "创建者" });
            MockContext.Object.UserRoles.Add(new UserRole { Id = 2, RoleDescription = "上传" });
            MockContext.Object.UserRoles.Add(new UserRole { Id = 3, RoleDescription = "下载" });

            MockContext.Object.Teams.Add(new Team { Id = 1, TeamName = "团队1", TeamDescription = "团队1描述" });
            MockContext.Object.Teams.Add(new Team { Id = 2, TeamName = "团队2", TeamDescription = "团队2描述" });

            MockContext.Object.TeamWithUsers.Add(new TeamWithUser { Id = 1, UserId = 1, TeamId = 1, UserRole = 1 });
            MockContext.Object.TeamWithUsers.Add(new TeamWithUser { Id = 2, UserId = 2, TeamId = 1, UserRole = 2 });
            MockContext.Object.TeamWithUsers.Add(new TeamWithUser { Id = 3, UserId = 2, TeamId = 2, UserRole = 1 });
            MockContext.Object.TeamWithUsers.Add(new TeamWithUser { Id = 4, UserId = 3, TeamId = 2, UserRole = 3 });
        }
    }
}
