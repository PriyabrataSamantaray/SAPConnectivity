using System;
using System.Data;
using System.Data.SqlClient;
using dbaccess;

namespace Calendar
{
    public static class CalendarUtils
    {
        public static Tuple<DateTime, DateTime> GetQuarterBounds(DateTime TestDate)
        {
            dbconn conn = new dbaccess.dbconn("CommonDb");
            conn.addParameter(new SqlParameter("@TestDate", TestDate));
            SqlDataReader dr = conn.dbReader("prcGetQrtrBoundsForDate",CommandType.StoredProcedure);

            dr.Read();

            Tuple<DateTime, DateTime> tpl = new Tuple<DateTime, DateTime>(Convert.ToDateTime(dr["BeginDate"]), Convert.ToDateTime(dr["EndDate"]));

            dr.Close();

            return tpl;
        }
    }
}
