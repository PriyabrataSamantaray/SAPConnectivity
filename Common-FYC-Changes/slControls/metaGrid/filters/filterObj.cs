using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace slControls.metaGrid.filters
{
    public class filterObj
    {
        private IEnumerable gridData;
        public Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
        private metaTotalRow totalRowData;
        private List<string> gapColumns = new List<string>();


        public filterObj(IEnumerable gridData, Dictionary<string, List<string>> filters, metaTotalRow totalRowData, List<string> _gapColumns)
        {
            this.gridData = gridData;
            this.filters = filters;
            this.totalRowData = totalRowData;
            this.gapColumns = _gapColumns;
            
        }
        public List<string> getSelection(string caption)
        {
            List<string> answer = new List<string>();

            if (filters.ContainsKey(caption))
                answer = filters[caption];

            return answer;
        }
        public List<string> showFilter(string caption)
        {

            Dictionary<string, List<string>> f = new Dictionary<string, List<string>>(filters);
            f.Remove(caption);


            List<string> data = new List<string>();

            foreach (object o in getData(f))
            {
                PropertyInfo pi = o.GetType().GetProperty(caption);
                if (pi.GetValue(o, null) == null)
                    break;
                else
                    data.Add(pi.GetValue(o, null).ToString());

            }

            return (from p in data orderby p select p).Distinct().ToList<string>();


        }

        public List<object> getData(Dictionary<string, List<string>> f)
        {
            List<object> data = gridData.Cast<object>().ToList();


            foreach (KeyValuePair<string, List<string>> kvp in f)
            {
                foreach (object o in gridData)
                {

                    PropertyInfo pi = o.GetType().GetProperty(kvp.Key);
                    if (pi.GetValue(o, null) == null)
                        break;
                    else
                    {
                        if (gapColumns.Contains(kvp.Key))
                        {
                            if (!kvp.Value.Contains(getColorFromValue(pi.GetValue(o, null).ToString())))
                                data.Remove(o);
                        }
                        else
                        {

                            if (!kvp.Value.Contains(pi.GetValue(o, null).ToString()))
                                data.Remove(o);
                        }
                    }

                }
            }


            if (totalRowData != null && data.Count > 0) //used for doing subtotals
            {
                PropertyInfo pi;
                object totalRow = Activator.CreateInstance(data[0].GetType());
                totalRowData.clearValues();

                foreach (KeyValuePair<string, string> kvp in totalRowData.specificAttributes)
                {
                    pi = totalRow.GetType().GetProperty(kvp.Key);
                    pi.SetValue(totalRow, kvp.Value, null);
                }
                foreach (object o in data)
                {
                    for (int i = 0; i < totalRowData.totalingColumns.Count(); i++)
                    {
                        pi = o.GetType().GetProperty(totalRowData.totalingColumns[i]);
                        if (pi.GetValue(o, null) == null)
                            break;
                        else
                        {
                            totalRowData.totalingValues[i] += decimal.Parse(pi.GetValue(o, null).ToString());
                        }

                    }
                }
                totalRowData.setSubtotalData();

                foreach (KeyValuePair<string, decimal> kvp in totalRowData.subtotalData)
                {
                    pi = totalRow.GetType().GetProperty(kvp.Key);
                    pi.SetValue(totalRow, kvp.Value, null);
                }

                //decimal result = 0;
                //foreach (object o in data)
                //{
                //    PropertyInfo pi = o.GetType().GetProperty("fte");
                //    result += decimal.Parse(pi.GetValue(o, null).ToString());

                //}



                //Type t = data[0].GetType();
                //object x = Activator.CreateInstance(t);

                //PropertyInfo propInfo = x.GetType().GetProperty("budgetUnit");
                //propInfo.SetValue(x, "ha", null);
                //propInfo = x.GetType().GetProperty("jobno");
                //propInfo.SetValue(x, "xxx", null);
                //propInfo = x.GetType().GetProperty("fte");
                //propInfo.SetValue(x, result, null);

                //propInfo = x.GetType().GetProperty("bkColor");
                //propInfo.SetValue(x, "yellow", null);
                //propInfo = x.GetType().GetProperty("drillableFTEColor");
                //propInfo.SetValue(x, "Black", null);

                data.Add(totalRow);



            }

           

            return data;



        }
        public string getColorFromValue(string value)
        {
            string result;

            if (value == "1.00")
                result = "green";
            else if (value == "0")
                result = "gray";
            else if (value == "")
                result = "blank";
            else if (decimal.Parse(value) > 1)
                result = "red";
            else
                result = "yellow";

            return result;

        }




        public List<object> applyFilter(string caption, List<string> selections)
        {
            //add new filter selection

            if (filters.ContainsKey(caption))
            {
                if (selections.Count > 0)
                    filters[caption] = selections;
                else
                    filters.Remove(caption);
            }
            else
            {
                if (selections.Count > 0)
                    filters.Add(caption, selections);
            }



            return getData(filters);

            //get all data 
            //List<object> data = gridData.Cast<object>().ToList();


            //foreach (KeyValuePair<string, List<string>> kvp in filters)
            //{
            //    foreach (object o in gridData)
            //    {

            //        PropertyInfo pi = o.GetType().GetProperty(kvp.Key);
            //        if (pi.GetValue(o, null) == null)
            //            break;
            //        else
            //        {
            //            if (! kvp.Value.Contains(pi.GetValue(o, null).ToString()))
            //                data.Remove(o);                          
            //        }
            //    }
            //}

            

            //return data;
        }
    }
}
