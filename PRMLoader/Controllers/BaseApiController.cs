using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices.AccountManagement;

namespace PRMLoader.Controllers
{
    public class BaseApiController : ApiController
    {

        public string FieldURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Field"];
            }
        }

        public string MyTeamURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MyTeam"];
            }
        }

        public string VarianURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Varian"];
            }
        }

        public string GetURL(string projectName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[projectName];
        }

        public string getBadgeFromUserIdentity(string userID)
        {
            // sample input format for userID = MPenny118243
            if (string.IsNullOrEmpty(userID))
                return null;

            string badge = string.Empty;
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                var usr = UserPrincipal.FindByIdentity(context, userID);//User.Identity.Name 
                if (usr != null)
                {
                    //badge = usr.DisplayName;
                    badge = usr.EmployeeId;
                }
            }
            return badge; // Sample output Badge = 118243
        }
    }
}
