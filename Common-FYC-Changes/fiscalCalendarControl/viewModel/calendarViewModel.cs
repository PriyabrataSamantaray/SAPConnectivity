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
using fiscalCalendar.prmExternalsSvc;

using System.ServiceModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reflection;

namespace fiscalCalendar.viewModel
{
    public class calendarViewModel : viewModelBase
    {
        private DateTime switchDate = new DateTime(2013, 1, 1);


        private bool _isEditable = false;
        public bool isEditable { get { return _isEditable; } set { SetProperty(ref _isEditable, value, "isEditable"); } }

        private string _monthName;
        public string monthName { get { return _monthName; } set { SetProperty(ref _monthName, value, "monthName"); } }

        private string _yearName;
        public string yearName { get { return _yearName; } set { SetProperty(ref _yearName, value, "yearName"); } }

        private fiscalData _startWeek;
        public fiscalData startWeek { get { return _startWeek; } set { SetProperty(ref _startWeek, value, "startWeek"); } }

        private fiscalData _endWeek;
        public fiscalData endWeek { get { return _endWeek; } set { SetProperty(ref _endWeek, value, "endWeek"); } }

        private fiscalData _oneWeek;
        public fiscalData oneWeek { get { return _oneWeek; } set { SetProperty(ref _oneWeek, value, "oneWeek"); } }

        private Visibility _calVisibility = Visibility.Collapsed;
        public Visibility calVisibility { get { return _calVisibility; } set { SetProperty(ref _calVisibility, value, "calVisibility"); } }

        private Visibility _headerVisibility = Visibility.Visible;
        public Visibility headerVisibility { get { return _headerVisibility; } set { SetProperty(ref _headerVisibility, value, "headerVisibility"); } }

        private List<fiscalData> _data;
        public List<fiscalData> data 
        { 
            get 
            { 
                return _data; 
            } 
            set 
            {
                SetProperty(ref _data, value, "data");

                if (data.Count < 14)
                {
                    calVisibility = Visibility.Collapsed;
                }
                else
                {
                    monthName = data[14].calDate.ToString("MMMM") + ",  " + data[14].calDate.ToString("yyyy");

                    yearName = "FY " + data[0].fiscalYear.ToString() + " Q" + data[0].fiscalQuarter.ToString();

                    int weeks = (from p in data select p.fiscalWeek).Distinct().Count();

                    calHeight = 22 + 28 * weeks - 1;

                    row5Visible = Visibility.Collapsed;
                    row6Visible = Visibility.Collapsed;

                    if (weeks > 4)
                        row5Visible = Visibility.Visible;
                    if (weeks > 5)
                        row6Visible = Visibility.Visible;

                    calVisibility = Visibility.Visible;
                }
            } 
        }

        private int _formHeight =209;
        public int formHeight { get { return _formHeight; } set { SetProperty(ref _formHeight, value, "formHeight"); } }

        private int _calHeight;
        public int calHeight { get { return _calHeight; } set { SetProperty(ref _calHeight, value, "calHeight"); } }

        private Thickness _gridStartPos = new Thickness(0, 40, 0, 0);
        public Thickness gridStartPos { get { return _gridStartPos; } set { SetProperty(ref _gridStartPos, value, "gridStartPos"); } }

        private Thickness _monthStringStartPos = new Thickness(0, 0, 0, 0);
        public Thickness monthStringStartPos { get { return _monthStringStartPos; } set { SetProperty(ref _monthStringStartPos, value, "monthStringStartPos"); } }

        private Visibility _amatWeekVisible = Visibility.Visible;
        public Visibility amatWeekVisible { get { return _amatWeekVisible; } set { SetProperty(ref _amatWeekVisible, value, "amatWeekVisible"); } }

        private Visibility _vseaWeekVisible = Visibility.Collapsed;
        public Visibility vseaWeekVisible { get { return _vseaWeekVisible; } set { SetProperty(ref _vseaWeekVisible, value, "vseaWeekVisible"); } }


        private Visibility _row5Visible = Visibility.Collapsed;
        public Visibility row5Visible { get { return _row5Visible; } set { SetProperty(ref _row5Visible, value, "row5Visible"); } }

        private Visibility _row6Visible = Visibility.Collapsed;
        public Visibility row6Visible { get { return _row6Visible; } set { SetProperty(ref _row6Visible, value, "row6Visible"); } }

        private bool _moveRightEnabled;
        public bool moveRightEnabled { get { return _moveRightEnabled; } set { SetProperty(ref _moveRightEnabled, value, "moveRightEnabled"); } }

        private bool _moveLeftEnabled;
        public bool moveLeftEnabled { get { return _moveLeftEnabled; } set { SetProperty(ref _moveLeftEnabled, value, "moveLeftEnabled"); } }

        private string _week1Bkgnd ="Transparent";
        public string week1Bkgnd { get { return _week1Bkgnd; } set { SetProperty(ref _week1Bkgnd, value, "week1Bkgnd"); } }

        private string _week2Bkgnd = "Transparent";
        public string week2Bkgnd { get { return _week2Bkgnd; } set { SetProperty(ref _week2Bkgnd, value, "week2Bkgnd"); } }

        private string _week3Bkgnd = "Transparent";
        public string week3Bkgnd { get { return _week3Bkgnd; } set { SetProperty(ref _week3Bkgnd, value, "week3Bkgnd"); } }

        private string _week4Bkgnd = "Transparent";
        public string week4Bkgnd { get { return _week4Bkgnd; } set { SetProperty(ref _week4Bkgnd, value, "week4Bkgnd"); } }

        private string _week5Bkgnd = "Transparent";
        public string week5Bkgnd { get { return _week5Bkgnd; } set { SetProperty(ref _week5Bkgnd, value, "week5Bkgnd"); } }

        private string _week6Bkgnd = "Transparent";
        public string week6Bkgnd { get { return _week6Bkgnd; } set { SetProperty(ref _week6Bkgnd, value, "week6Bkgnd"); } }

        private int _week1Wdth = 1;
        public int week1Wdth { get { return _week1Wdth; } set { SetProperty(ref _week1Wdth, value, "week1Wdth"); } }

        private int _week2Wdth = 1;
        public int week2Wdth { get { return _week2Wdth; } set { SetProperty(ref _week2Wdth, value, "week2Wdth"); } }

        private int _week3Wdth = 1;
        public int week3Wdth { get { return _week3Wdth; } set { SetProperty(ref _week3Wdth, value, "week3Wdth"); } }

        private int _week4Wdth = 1;
        public int week4Wdth { get { return _week4Wdth; } set { SetProperty(ref _week4Wdth, value, "week4Wdth"); } }

        private int _week5Wdth = 1;
        public int week5Wdth { get { return _week5Wdth; } set { SetProperty(ref _week5Wdth, value, "week5Wdth"); } }

        private int _week6Wdth = 1;
        public int week6Wdth { get { return _week6Wdth; } set { SetProperty(ref _week6Wdth, value, "week6Wdth"); } }

        private string _week1BrdColor = "Black";
        public string week1BrdColor { get { return _week1BrdColor; } set { SetProperty(ref _week1BrdColor, value, "week1BrdColor"); } }

        private string _week2BrdColor = "Black";
        public string week2BrdColor { get { return _week2BrdColor; } set { SetProperty(ref _week2BrdColor, value, "week2BrdColor"); } }

        private string _week3BrdColor = "Black";
        public string week3BrdColor { get { return _week3BrdColor; } set { SetProperty(ref _week3BrdColor, value, "week3BrdColor"); } }

        private string _week4BrdColor = "Black";
        public string week4BrdColor { get { return _week4BrdColor; } set { SetProperty(ref _week4BrdColor, value, "week4BrdColor"); } }

        private string _week5BrdColor = "Black";
        public string week5BrdColor { get { return _week5BrdColor; } set { SetProperty(ref _week5BrdColor, value, "week5BrdColor"); } }

        private string _week6BrdColor = "Black";
        public string week6BrdColor { get { return _week6BrdColor; } set { SetProperty(ref _week6BrdColor, value, "week6BrdColor"); } }




        private fiscalData nextPeriod;
        private fiscalData prevPeriod;
        private fiscalData nowPeriod;
        private ObservableCollection<fiscalData> allData;



        public calendarViewModel()
        {
            if (IsDesignTime)
            {
                calVisibility = Visibility.Visible;
                calHeight = 189;
                row5Visible = Visibility.Visible;
                row6Visible = Visibility.Visible;
                amatWeekVisible = Visibility.Visible;
                formHeight = 265;
            }
  
        }


        public void moveNextPeriod(bool forward)
        {
            week1Wdth = 1; week2Wdth = 1; week3Wdth = 1; week4Wdth = 1; week5Wdth = 1; week6Wdth = 1;

            if(forward)
                nowPeriod = (from p in allData where p.fiscalPeriod == nextPeriod.fiscalPeriod && p.fiscalYear == nextPeriod.fiscalYear select p).First<fiscalData>();
            else
                nowPeriod = (from p in allData where p.fiscalPeriod == prevPeriod.fiscalPeriod && p.fiscalYear == prevPeriod.fiscalYear select p).First<fiscalData>();

            getNextPrevious();

            data = (from p in allData
                    where p.fiscalPeriod == nowPeriod.fiscalPeriod && p.fiscalYear == nowPeriod.fiscalYear
                    select p).ToList<fiscalData>();
        }

        public void clearWeekBkgnd()
        {
            week1Bkgnd = "Transparent"; week2Bkgnd = "Transparent"; week3Bkgnd = "Transparent"; week4Bkgnd = "Transparent";
            week5Bkgnd = "Transparent"; week6Bkgnd = "Transparent";
        }


        public void clearAll()
        {

            week1Wdth = 1; week2Wdth = 1; week3Wdth = 1; week4Wdth = 1; week5Wdth = 1; week6Wdth = 1;



        }
        public void setNewEndWeek(fiscalData _endWeek)
        {
            int rc = 0;

            endWeek = _endWeek;

            for (int i = 0; i < data.Count; i += 7)
            {
                rc += 1;

                if (endWeek.CompareTo(data[i]) == 0)  //if start week is found in this month
                {
                    this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Red", null);
                    this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 5, null);
                }
                else
                {
                    if (data[i].CompareTo(startWeek) != 0) //else if not the start week clear out the color
                    {
                        this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Black", null);
                        this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 1, null);
                    }

                }

            }


        }
        public void setStartandEndWeek()
        {
            startWeek = new fiscalData();
            endWeek = new fiscalData();

            setWeekBkgnd("week1");
            getWeekLabel("week1");

            string endWeekStr = "week" + (data.Count() / 7).ToString();

            setWeekBkgnd(endWeekStr);
            getWeekLabel(endWeekStr);
        }

        public void setNewOneWeek(fiscalData _oneWeek)
        {
            if (_oneWeek.displayWeek == "")
                return;

            int rc = 0;

            oneWeek = _oneWeek;

            for (int i = 0; i < data.Count; i += 7)
            {
                rc += 1;

                if (oneWeek.CompareTo(data[i]) == 0)  //if start week is found in this month
                {
                    this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Blue", null);
                    this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 5, null);
                }
                else
                {
                    if (data[i].CompareTo(oneWeek) != 0) //else if not the end week clear out the color
                    {
                        this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Black", null);
                        this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 1, null);
                    }

                }

            }


        }


        public void setNewStartWeek(fiscalData _startWeek)
        {
            int rc = 0;

            startWeek = _startWeek;

            for (int i = 0; i < data.Count; i += 7)
            {
                rc += 1;

                if (startWeek.CompareTo(data[i]) == 0)  //if start week is found in this month
                {
                    this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Green", null);
                    this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 5, null);
                }
                else
                {
                    if (data[i].CompareTo(endWeek) != 0) //else if not the end week clear out the color
                    {
                        this.GetType().GetProperty("week" + rc + "BrdColor").SetValue(this, "Black", null);
                        this.GetType().GetProperty("week" + rc + "Wdth").SetValue(this, 1, null);
                    }

                }

            }


            

        }



        public void displayWeekBkgnd(string week)
        {

            clearWeekBkgnd();

            this.GetType().GetProperty(week + "Bkgnd").SetValue(this, "yellow", null);

        }
        public void setWeekBkgnd(string week)
        {


            if (oneWeek != null)
            {
                oneWeek = getWeek(week);
                return;
            }


            if (startWeek == null)
            {
                clearAll();
                this.GetType().GetProperty(week + "Wdth").SetValue(this, 5, null);

                return;
            }
            fiscalData selectedWeek = getWeek(week);


            if (startWeek.displayWeek == "")
                startWeek = selectedWeek;
            else if (endWeek.displayWeek == "")
            {
                if (selectedWeek.CompareTo(startWeek) == 0)
                    endWeek = selectedWeek;
                else if (selectedWeek.CompareTo(startWeek) < 1)
                    startWeek = selectedWeek;
                else
                    endWeek = selectedWeek;
            }
            else
            {
                startWeek = selectedWeek;
                endWeek = new fiscalData();

            }

            

        }
        public string getWeekLabel(string week)
        {
            fiscalData d = getWeek(week);
            return d.fiscalYear.ToString() + "-" + d.displayWeek;
        }



        public fiscalData getWeek(string week)
        {
            string displayWeek;
            int intWeek;
            int year;

            int arrayOffset = int.Parse(week.Replace("week", ""));

            arrayOffset = (arrayOffset - 1) * 7;


            displayWeek = data[arrayOffset].displayWeek;
            intWeek = data[arrayOffset].fiscalWeek;
            year = data[arrayOffset].fiscalYear;



            fiscalData d = new fiscalData(intWeek, year, displayWeek);


            return d;
        }


        public void setData(ObservableCollection<fiscalData> d)
        {
            allData = d;

            nowPeriod = (from p in allData where p.calDate == DateTime.Today select p).First<fiscalData>();

            getNextPrevious();

            data = (from p in allData
                    where p.fiscalPeriod == nowPeriod.fiscalPeriod && p.fiscalYear == nowPeriod.fiscalYear
                    select p).ToList<fiscalData>();

            isEditable = true;
        }
        public void setSmallData(List<fiscalData> d)
        {
            gridStartPos = new Thickness(0, 0, 0, 0);
            monthStringStartPos = new Thickness(26, 0, 0, 0);
            headerVisibility = Visibility.Collapsed;
            data = d;


        }

        private void getNextPrevious()
        {
            int prvousPeriod, prvousYear, nxtPer, nxtYear;

            if (nowPeriod.fiscalPeriod == 1)
            {
                if (nowPeriod.fiscalYear == 2016)
                {
                    prvousPeriod = 6;
                }
                else
                {
                    prvousPeriod = 12;
                }
                prvousYear = nowPeriod.fiscalYear - 1;
            }
            else
            {
                prvousPeriod = nowPeriod.fiscalPeriod - 1;
                prvousYear = nowPeriod.fiscalYear;
            }

            if (nowPeriod.fiscalPeriod == 12 || (nowPeriod.fiscalPeriod == 6 && nowPeriod.fiscalYear == 2015))
            {
                nxtPer = 1;
                nxtYear = nowPeriod.fiscalYear + 1;
            }
            else
            {
                nxtPer = nowPeriod.fiscalPeriod + 1;
                nxtYear = nowPeriod.fiscalYear;
            }

            try
            {
                if ((from p in allData where p.fiscalYear == nxtYear && p.fiscalPeriod == nxtPer select p).Count() >= 14)
                {
                    nextPeriod = (from p in allData where p.fiscalYear == nxtYear && p.fiscalPeriod == nxtPer select p).First<fiscalData>();
                    moveRightEnabled = true;
                }
                else
                {
                    nextPeriod = null;
                    moveRightEnabled = false;
                }
            }
            catch
            {
                nextPeriod = null;
                moveRightEnabled = false;
            }
            try
            {
                prevPeriod = (from p in allData where p.fiscalYear == prvousYear && p.fiscalPeriod == prvousPeriod select p).First<fiscalData>();
                moveLeftEnabled = true;
            }
            catch
            {
                prevPeriod = null;
                moveLeftEnabled = false;
            }


        }


    }
}
