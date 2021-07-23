using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRMLoader.Models.structs;

namespace PRMLoader.Models
{
    public class HomeData : commonDAL
    {
        public string user;
        public string sqlServer;
        public string fullName;
        public string lastName;
        public string firstName;
        public string department;
        public string badge;
        public List<ProjectDetails> accessibleProjects;
        public string system;
        public string error;
        public List<adStruct> adSets;
        //public List<string> allAreas;
        public bool hasAccessToApplication;

        public HomeData() //ctor
        {
            system = base.getEnvironment();
            accessibleProjects = new List<ProjectDetails>();
        }

        internal HomeData getHomeData(string userBadge)
        {
            HomeData hd = new HomeData();
            try
            {
                sql = string.Format("SELECT TOP 1 FIRSTNAME, LASTNAME, FIRSTNAME + ' ' + LASTNAME AS FullName, Badge, DEPARTMENT FROM Employees WHERE Badge = '{0}'", userBadge);
                dt = dbc.dbTable(sql);
                hd.firstName = dt.Rows[0]["FIRSTNAME"].ToString();
                hd.lastName = dt.Rows[0]["LASTNAME"].ToString();
                hd.fullName = dt.Rows[0]["FullName"].ToString();
                hd.department = dt.Rows[0]["DEPARTMENT"].ToString();
                hd.badge = userBadge;
            }
            catch
            {
                hd.error = "No Database Access";
                return hd;
            }
            hd.error = ""; //error means no access
            hd.sqlServer = system;  //production qa or dev
            hd.sqlServer = sqlserver;  //machine name
            hd.adSets = getADData();  //All Ad info
            return hd;
        }

        internal bool HasAccessToGFPPRM(HomeData hd)
        {
            sql = string.Format("SELECT COUNT(*) FROM v_gfpCostCenters WHERE Department= '{0}'", hd.department);
            int val = dbc.dbVal(sql);
            bool hasAccess = val > 0;
            if (!hasAccess) {
                sql = string.Format("SELECT COUNT(*) FROM v_gfpBUUsers WHERE buOps = '{0}' OR financeController ='{0}' OR buGm = '{0}'", hd.badge);
                int val1 = dbc.dbVal(sql);
                hasAccess = val1 > 0;
            }
            return hasAccess;
        }

        internal bool HasAccessToField(HomeData hd)
        {
            sql = string.Format("SELECT COUNT(*) FROM v_pseSecurity WHERE employeeID= '{0}'", hd.badge);
            int val = dbc.dbVal(sql);
            return val > 0;
        }
    }
}