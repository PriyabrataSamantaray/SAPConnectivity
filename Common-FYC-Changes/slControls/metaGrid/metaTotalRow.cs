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
using System.Collections.Generic;

namespace slControls.metaGrid
{
    public class metaTotalRow
    {
        public Dictionary<string, string> specificAttributes;
        public string[] totalingColumns;
        public string[] lesserTotalingColumns;
        public decimal[] totalingValues;
        public Dictionary<string, decimal> subtotalData;
       


        public metaTotalRow()
        {

        }

        public metaTotalRow(List<string> totalingColumns, List<string> lesserTotalsColumns, Dictionary<string, string> specificAttributes):this(totalingColumns, specificAttributes)
        {
            this.lesserTotalingColumns = lesserTotalsColumns.ToArray();
        }

        public metaTotalRow(List<string> totalingColumns, Dictionary<string, string> specificAttributes) : this(totalingColumns)
        {
            
            this.specificAttributes = specificAttributes;
            //this.totalingColumns = totalingColumns.ToArray();
            //this.totalingValues = new decimal[totalingColumns.Count];
        
        }
        public metaTotalRow(List<string> totalingColumns)
        {
            this.totalingColumns = totalingColumns.ToArray();
            this.totalingValues = new decimal[totalingColumns.Count];
        }

        public void swap(bool moveToSmaller)
        {
            if (!moveToSmaller)
            {
                if (totalingColumns.Length > lesserTotalingColumns.Length)
                    return;
            }


            //back up large set
            string[] tmp = new string[totalingColumns.Length];
            Array.Copy(totalingColumns, tmp, totalingColumns.Length);
            //copy small set into large set
            totalingColumns = new string[lesserTotalingColumns.Length];
            Array.Copy(lesserTotalingColumns, totalingColumns, lesserTotalingColumns.Length);
            //copy large set into small set
            lesserTotalingColumns = new string[tmp.Length];
            Array.Copy(tmp, lesserTotalingColumns, tmp.Length);
         


            this.totalingValues = new decimal[totalingColumns.Length];

        }


        public void setSubtotalData()
        {
            subtotalData = new Dictionary<string, decimal>();
            for (int i = 0; i < totalingColumns.Length; i++)
                subtotalData.Add(totalingColumns[i], totalingValues[i]);

        }
        public void clearValues()
        {
            for (int i = 0; i < totalingValues.Length; i++)
                totalingValues[i] = 0;
        }

    }
}
