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

namespace fiscalCalendar
{
    public class fiscalData: IComparable
    {
        public DateTime calDate { get; set; }
        public int fiscalWeek { get; set; }
        public int fiscalPeriod { get; set; }
        public int fiscalQuarter { get; set; }
        public int fiscalYear { get; set; }
        public bool workDay { get; set; }
        public string displayNum { get; set; }
        public string displayWeek { get; set; }
        public string displayBgnd { get; set; }
        public string displayQtr { get; set; }
        public string displayFiscalWeek { get; set; }

        public fiscalData(DateTime _calDate, int _fiscalWeek, int _fiscalPeriod, int _fiscalQuarter, int _fiscalYear, bool _workDay)
        {
            this.calDate = _calDate;
            this.fiscalWeek = _fiscalWeek;
            this.fiscalPeriod = _fiscalPeriod;
            this.fiscalQuarter = _fiscalQuarter;
            this.fiscalYear = _fiscalYear;
            this.workDay = _workDay;

            this.displayNum = this.calDate.Day.ToString();
            this.displayWeek = _fiscalWeek.ToString();

            if (_workDay)
                displayBgnd = "Transparent";
            else
                displayBgnd = "Aquamarine";


            this.displayQtr = "FY" + fiscalYear.ToString().Substring(2, 2) + "-Q" + fiscalQuarter.ToString();

            this.displayFiscalWeek = this.fiscalYear.ToString() + "-" + this.displayWeek;
        }

        public fiscalData()
        {
            this.fiscalYear = 0;
            this.fiscalWeek = 0;
            this.displayWeek = "";
        }

        public fiscalData(int _fiscalWeek, int _fiscalYear, string _displayWeek)
        {
            this.displayWeek = _displayWeek;
            this.fiscalWeek = _fiscalWeek;
            this.fiscalYear = _fiscalYear;
            this.displayFiscalWeek = this.fiscalYear.ToString() + "-" + this.displayWeek;

        }



        public int CompareTo(object obj)
        {
            if (obj == null)
                return -1;

            fiscalData otherData = (fiscalData)obj;

            if (this.fiscalYear == otherData.fiscalYear)
                return this.fiscalWeek.CompareTo(otherData.fiscalWeek);
            else
                return this.fiscalYear.CompareTo(otherData.fiscalYear);
        }

    }
}
