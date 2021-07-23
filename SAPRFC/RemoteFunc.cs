using System;
using System.Data;     // Must use .NET Framework 4.0 or the namespace won't be found
using SAP.Middleware.Connector;

namespace SAPRFC
{
    public class RemoteFunc
    {
        protected IRfcFunction _function = null;
        protected string _TableName;
        protected RfcDestination _Dest;

        public RemoteFunc(string serverID, string RFCName, string IRfcTableName)
        {
            _TableName = IRfcTableName;

            try
            {
                // Get a reference to the SAP needed server
                _Dest = GetDestination(serverID);

                //Get metadata repository associated with the destination (server)
                _function = _Dest.Repository.CreateFunction(RFCName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void AddParameter(string ParamName, object ParamValue)
        {
            _function.SetValue(ParamName, ParamValue);
        }

        protected RfcDestination GetDestination(string SAPServer)
        {
            // Create the destination.  Config info is read from the App.config.
            RfcDestination destination = RfcDestinationManager.GetDestination(SAPServer);

            return destination;
        }
        protected DataTable getData()
        {
            //Moved this to its own method so it can be used for return data
            // as well as return values 

            // Retrieve the data from the function as an IRfcTable
            IRfcTable _ReturnedData = _function.GetTable(_TableName);

            DataTable dt = new DataTable();

            for (int i = 0; i < _ReturnedData.ElementCount; i++)
            {
                // Get metadata for column definitions
                RfcElementMetadata md = _ReturnedData.GetElementMetadata(i);

                // Map SAP data types to C#, and create columns of the corresponding type
                // in the table of return data
                Type T = GetSystemType(md.DataType);
                dt.Columns.Add(md.Name, T);
            }

            foreach (IRfcStructure IrStruct in _ReturnedData)
            {
                DataRow r = dt.NewRow();

                string[] bogusDates = { "", "00000000", "0000-00-00" };


                // Call correct method for returning data by testing the data type of the target column
                for (int i = 0; i < _ReturnedData.ElementCount; i++)
                {//r[i] = IrStruct[i].GetString();
                    if (dt.Columns[i].DataType == System.Type.GetType("System.String"))
                    {
                        r[i] = IrStruct[i].GetString();
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.Byte"))
                    {
                        r[i] = IrStruct[i].GetByte();
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                    {
                        string dte = IrStruct[i].GetString();

                        if (Array.IndexOf(bogusDates, dte) != -1)
                            dte = "12/31/1999";


                        r[i] = DateTime.Parse(dte);
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.Decimal"))
                    {
                        r[i] = IrStruct[i].GetDecimal();
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.Int16"))
                    {
                        r[i] = IrStruct[i].GetShort();
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.Int32"))
                    {
                        r[i] = IrStruct[i].GetInt();
                    }
                    else if (dt.Columns[i].DataType == System.Type.GetType("System.Int64"))
                    {
                        r[i] = IrStruct[i].GetLong();
                    }
                }

                dt.Rows.Add(r);
            }

            return dt;


        }

        public DataTable Invoke()
        {
            //Make the remote call
            _function.Invoke(_Dest);
            return getData();

        }

        // Map SAP data types to C# types
        private Type GetSystemType(RfcDataType rfcType)
        {
            string SystemType;

            switch (rfcType)
            {
                case RfcDataType.BYTE:
                case RfcDataType.INT1:
                    SystemType = "Byte";
                    break;
                case RfcDataType.DATE:
                    SystemType = "DateTime";
                    break;
                case RfcDataType.DECF16:
                case RfcDataType.DECF34:
                case RfcDataType.FLOAT:
                    SystemType = "Decimal";
                    break;
                case RfcDataType.INT2:
                    SystemType = "Int16";
                    break;
                case RfcDataType.INT4:
                    SystemType = "Int32";
                    break;
                case RfcDataType.INT8:
                    SystemType = "Int64";
                    break;
                default:
                    SystemType = "String";
                    break;
            }

            return System.Type.GetType("System." + SystemType);
        }
    }
}