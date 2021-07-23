using dbaccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using PRMLoader.Models.structs;

namespace PRMLoader.Models
{
    public class commonDAL
    {
        public string sqlserver;
        public string database;
        public dbconn dbc;
        public string sql;
        public DataTable dt;
        public const string devEnv = ".";
        public const string qaEnv = "DCA-QA-246";//test DCA-QA-86 
        public const string proEnv = "DCA-APP-1051";
        public const string proEnv2 = "DCA-APP-1052";
        public commonDAL()
        {
            database = "CommonDB";
            sqlserver = Dns.GetHostName();
            dbc = new dbconn(sqlserver, database);
        }

        public string getEnvironment()
        {
            string system = "DEV";
            switch (sqlserver)
            {
                case proEnv:
                    system = "Production";
                    break;
                case proEnv2:
                    system = "Production";
                    break;
                case qaEnv:
                    system = "QA";
                    break;
                default:
                    system = "DEV";
                    break;
            }
            return system;
        }



        public List<adStruct> getADData()
        {
            List<adStruct> adSet = new List<adStruct>();

            sql = "SELECT * FROM v_adGroups";
            DataTable dt = dbc.dbTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                adSet.Add(new adStruct(row["adName"].ToString(), row["privilege"].ToString(), row["functionalArea"].ToString()));
            }

            return adSet;
        }
    }
}