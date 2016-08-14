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
        public ActionResult List()
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
            var team = teamList == null ? new TeamViewModel() { } : teamList.First();
            ViewBag.TeamName = team.TeamName;
            ViewBag.TeamId = team.TeamId;
            ViewBag.FileList = null;
            return View(teamList);
        }
    }
}
