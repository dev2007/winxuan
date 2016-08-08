using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winxuan.Infrastructure;
using Winxuan.Infrastructure.DTO;
using Winxuan.Web.Models;

namespace Winxuan.Web.Controllers
{
    public class TeamController : BaseController
    {
        /// <summary>
        /// User's team list page.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult List()
        {
            ResponseJson<IEnumerable<UserTeamDTO>> list = WebUtils.Get<IEnumerable<UserTeamDTO>>(string.Format("{0}/{1}/{2}", ApiServer, "api/UserTeam", Session["userid"]), GetCookieToken());
            IEnumerable<TeamViewModel> teamList = null;
            if (list.Status)
            {
                teamList = list.Data.Select(t => new TeamViewModel
                {
                    UserId = t.UserId,
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    TeamDescription = t.TeamDescription,
                    RoleId = t.RoleId,
                    RoleDescription = t.RoleDescription
                });
            }
            ViewBag.TeamName = teamList.First().TeamName;
            ViewBag.FileList = null;
            return PartialView("List", teamList);
        }
    }
}
