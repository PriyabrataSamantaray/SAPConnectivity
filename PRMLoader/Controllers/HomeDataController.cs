using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PRMLoader.Models;
using PRMLoader.Models.structs;

namespace PRMLoader.Controllers
{
    public class HomeDataController : BaseApiController
    {
        enum ProjectName
        {
            MyTeam,
            Field,
            Varian
        }
        public HomeData Get()
        {
            HomeData homeData = new HomeData();
            string user = HttpContext.Current.User.Identity.Name;
            homeData = homeData.getHomeData(getBadgeFromUserIdentity(user));
            homeData.hasAccessToApplication = false;

            List<adStruct> userGroups = new List<adStruct>();
            foreach (adStruct ad in homeData.adSets)
            {
                if (HttpContext.Current.User.IsInRole(ad.adName))
                {
                    bool exitFromLoop = false;
                    switch (ad.adArea.ToUpper())
                    {                       
                        case "GFP":
                            {
                                addProject(homeData.accessibleProjects, ProjectName.MyTeam.ToString());            
                            }
                            break;
                        case "PSE"://Field
                            {
                                addProject(homeData.accessibleProjects, ProjectName.Field.ToString());
                            }
                            break;
                        case "ENGINEERING":
                            {
                                addProject(homeData.accessibleProjects, ProjectName.Varian.ToString());
                            }
                            break;
                    }

                    if (exitFromLoop)
                    {
                        break;
                    }
                }
            }

            if (homeData.accessibleProjects.Count > 0)
            {
                homeData.hasAccessToApplication = true;
                bool myTeamAppAdded = false;
                bool fieldAppAdded = false;
                foreach (var item in homeData.accessibleProjects)
                {
                    if (item.ProjectName == ProjectName.MyTeam.ToString())
                    {
                        myTeamAppAdded = true;
                    }

                    if (item.ProjectName == ProjectName.Field.ToString())
                    {
                        fieldAppAdded = true;
                    }
                }

                if (!myTeamAppAdded)
                {
                    if (homeData.HasAccessToGFPPRM(homeData))
                    {
                        addProject(homeData.accessibleProjects, ProjectName.MyTeam.ToString());
                    }
                }

                if (!fieldAppAdded)
                {
                    if (homeData.HasAccessToField(homeData))
                    {
                        addProject(homeData.accessibleProjects, ProjectName.Field.ToString());
                    }
                }
            }
            else
            {
                if (homeData.HasAccessToGFPPRM(homeData))
                {
                    homeData.hasAccessToApplication = true;
                    addProject(homeData.accessibleProjects, ProjectName.MyTeam.ToString());
                }

                if (homeData.HasAccessToField(homeData))
                {
                    homeData.hasAccessToApplication = true;
                    addProject(homeData.accessibleProjects, ProjectName.Field.ToString());
                }
            }
            homeData.adSets = userGroups;
            return homeData;
        }

        private void addProject(List<ProjectDetails> accessibleProjects, string projectName)
        {
            if (!accessibleProjects.Any(proj => proj.ProjectName == projectName))
            {
                accessibleProjects.Add(new ProjectDetails { ProjectName = projectName, URL = GetURL(projectName) });
            }
        }
    }
}
