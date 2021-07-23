using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbaccess
{
    class serverSelector
    {
        private Dictionary<string, string> servers;

        public serverSelector()
        {
            servers = new Dictionary<string, string>();
            servers.Add("DCA-QA-246", "DCA-QA-247"); //QA
            //servers.Add("2S7RVF2", "DCA-QA-247");
            servers.Add("DCA-APP-1051", "PRMPRDLSNR"); //PRD1
            servers.Add("DCA-APP-1052", "PRMPRDLSNR"); //PRD2
            servers.Add("DCA-APP-1062", "PRMPRDLSNR"); //PRD1 TimeCard
            servers.Add("DCA-APP-1063", "PRMPRDLSNR"); //PRD2 TimeCard

        }


        public string ConnString(string server, string db)
        {
            string retString;

            /*
             * Now using SQL Security for QA & PRD environments
             * 
             * was using the IIS account on the SQL Server like this  user= AMAT\dca-qa-246$
             * 
             */

            if (servers.ContainsKey(server.ToUpper()))
            {
                server = servers[server.ToUpper()];
                retString = "Server=" + server + "; Database=" + db + ";User Id='prmuser'; Password='2#prmlogin!'";
            }
            else
            {
                retString = "Data Source=" + server + ";Integrated Security = true;Initial Catalog=" + db;
            }




            return retString;
        }




    }
}
