using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace dbaccess
{
    public class BulkSQL
    {
        private SqlBulkCopy _BulkCopy;
        private List<string> _Headings;
        private List<string> _DataTypes;
        private DataTable _TempData;
        private serverSelector servSelector;

        public DataTable TempData {get{return _TempData;} set{_TempData=value;}}
        private SqlBulkCopy BulkCopy{get{return _BulkCopy;}set{_BulkCopy=value;}}
        private List<string>Headings{get{return _Headings;}set{_Headings=value;}}
        private List<string>DataTypes{get{return _DataTypes;}set{_DataTypes=value;}}

        //Build the source data table using supplied headings and data types
        public BulkSQL(string server, string db, List<string> Headings, List<string> DataTypes)
        {
            servSelector = new serverSelector();

            string constr = servSelector.ConnString(server, db);



            // Make sure that there is a data type for each column, and vice-versa
            if (Headings.Count != DataTypes.Count)
                throw new ArgumentException("'Heading' and 'DataType' args must contain the same number of elements!");

            // Store table metadata
            this.Headings = Headings;
            this.DataTypes = DataTypes;

            // Create a new SQL bulk copy object
            BulkCopy = new SqlBulkCopy(constr);

            // Add column mappings.  Assume that source and destination columns have the same names and case.
            for (int i = 0; i < Headings.Count; i++)
                BulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(Headings[i], Headings[i]));

            // Use column metadata to create a in-memory store
            TempData = CreateDataTable();
        }

        //Use the supplied data table as the source for the bulk insert
        public BulkSQL(string server, string db, DataTable Source)
        {
            servSelector = new serverSelector();

            string constr = servSelector.ConnString(server, db);


            TempData = Source;

            // Create a new SQL bulk copy object
            BulkCopy = new SqlBulkCopy(constr);

            // Add column mappings.  Assume that source and destination columns have the same names and case.
            // Columns are individually mapped, in case the destination table contains more columns than the source
            // (the columns would otherwise be mapped automatically)
            for (int i = 0; i < Source.Columns.Count; i++)
            {
                string ColName = Source.Columns[i].ToString();
                BulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(ColName, ColName));
            }
        }

        // When a column name in the destination table differs from the source (in-memory DataTable),
        // the column can be re-mapped using this method.
        public void ReMapDestColumn(string OldDestColumn, string NewDestColumn)
        {
            foreach (SqlBulkCopyColumnMapping M in BulkCopy.ColumnMappings)
            {
                if (M.DestinationColumn == OldDestColumn)
                {
                 M.DestinationColumn = NewDestColumn;

                 break;
                }
            }
        }

        // Create a temporary data table in memory for use by the bulk insert operation
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < Headings.Count; i++)
                dt.Columns.Add(Headings[i], System.Type.GetType("System." + DataTypes[i]));

            return dt;
        }

        // Perform the final insert operation against the target SQL table
        public void Insert(string DestinationTable)
        {
            BulkCopy.DestinationTableName = DestinationTable;
            BulkCopy.WriteToServer(TempData);
        }
    }
}
