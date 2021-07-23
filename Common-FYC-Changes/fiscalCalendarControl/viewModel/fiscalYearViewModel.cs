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
using System.Collections.ObjectModel;
using fiscalCalendar.prmExternalsSvc;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace fiscalCalendar.viewModel
{
    public class fiscalYearViewModel : viewModelBase
    {
        private ObservableCollection<fiscalData> allData;

        private List<fiscalData> _per1;
        public List<fiscalData> per1 { get { return _per1; } set { SetProperty(ref _per1, value, "per1"); } }
        private List<fiscalData> _per2;
        public List<fiscalData> per2 { get { return _per2; } set { SetProperty(ref _per2, value, "per2"); } }
        private List<fiscalData> _per3;
        public List<fiscalData> per3 { get { return _per3; } set { SetProperty(ref _per3, value, "per3"); } }

        private List<fiscalData> _per4;
        public List<fiscalData> per4 { get { return _per4; } set { SetProperty(ref _per4, value, "per4"); } }
        private List<fiscalData> _per5;
        public List<fiscalData> per5 { get { return _per5; } set { SetProperty(ref _per5, value, "per5"); } }
        private List<fiscalData> _per6;
        public List<fiscalData> per6 { get { return _per6; } set { SetProperty(ref _per6, value, "per6"); } }

        private List<fiscalData> _per7;
        public List<fiscalData> per7 { get { return _per7; } set { SetProperty(ref _per7, value, "per7"); } }
        private List<fiscalData> _per8;
        public List<fiscalData> per8 { get { return _per8; } set { SetProperty(ref _per8, value, "per8"); } }
        private List<fiscalData> _per9;
        public List<fiscalData> per9 { get { return _per9; } set { SetProperty(ref _per9, value, "per9"); } }

        private List<fiscalData> _per10;
        public List<fiscalData> per10 { get { return _per10; } set { SetProperty(ref _per10, value, "per10"); } }
        private List<fiscalData> _per11;
        public List<fiscalData> per11 { get { return _per11; } set { SetProperty(ref _per11, value, "per11"); } }
        private List<fiscalData> _per12;
        public List<fiscalData> per12 { get { return _per12; } set { SetProperty(ref _per12, value, "per12"); } }


        private string _qtr1Lbl;
        public string qtr1Lbl { get { return _qtr1Lbl; } set { SetProperty(ref _qtr1Lbl, value, "qtr1Lbl"); } }
        private string _qtr2Lbl;
        public string qtr2Lbl { get { return _qtr2Lbl; } set { SetProperty(ref _qtr2Lbl, value, "qtr2Lbl"); } }
        private string _qtr3Lbl;
        public string qtr3Lbl { get { return _qtr3Lbl; } set { SetProperty(ref _qtr3Lbl, value, "qtr3Lbl"); } }
        private string _qtr4Lbl;
        public string qtr4Lbl { get { return _qtr4Lbl; } set { SetProperty(ref _qtr4Lbl, value, "qtr4Lbl"); } }

        private List<string> _displayYrs;
        public List<string> displayYrs { get { return _displayYrs; } set { SetProperty(ref _displayYrs, value, "displayYrs"); } }

        private fiscalData _startWeek = new fiscalData();
        public fiscalData startWeek { get { return _startWeek; } set { SetProperty(ref _startWeek, value, "startWeek"); } }
        private fiscalData _endWeek = new fiscalData();
        public fiscalData endWeek { get { return _endWeek; } set { SetProperty(ref _endWeek, value, "endWeek"); } }

        private fiscalData _oneWeek = new fiscalData();
        public fiscalData oneWeek { get { return _oneWeek; } set { SetProperty(ref _oneWeek, value, "oneWeek"); } }

        private Visibility _multiWeekVisibility = Visibility.Visible;
        public Visibility multiWeekVisibility { get { return _multiWeekVisibility; } set { SetProperty(ref _multiWeekVisibility, value, "multiWeekVisibility"); } }

        private Visibility _oneWeekVisibility = Visibility.Collapsed;
        public Visibility oneWeekVisibility { get { return _oneWeekVisibility; } set { SetProperty(ref _oneWeekVisibility, value, "oneWeekVisibility"); } }

        private string _selectedYear;
        public string selectedYear 
        { 
            get 
            { 
                return _selectedYear; 
            } 
            set 
            { 
                SetProperty(ref _selectedYear, value, "selectedYear");


                for (int i = 1; i < 13; i++)
                {
                    List<fiscalData> tmp = (from p in allData
                                               where p.fiscalPeriod == i && p.fiscalYear == int.Parse(value)
                                            select p).ToList<fiscalData>();

                    this.GetType().GetProperty("per" +i).SetValue(this, tmp, null);


                }
                //needed to re-display the start and end weeks around the new data

                fiscalData tmpEndWeek = endWeek;
                endWeek = new fiscalData();
                endWeek = tmpEndWeek;
                fiscalData tmpStartWeek = startWeek;
                startWeek = new fiscalData();
                startWeek = tmpStartWeek;

                var quarterdetails = (from m in allData
                                      where m.fiscalYear == Convert.ToInt32(value)
                                      select m.fiscalQuarter).Distinct();

                if (quarterdetails != null)
                {
                    switch (quarterdetails.Count())
                    {
                        case 1:
                            qtr1Lbl = value + " - Q1";
                            qtr2Lbl = "";
                            qtr3Lbl = "";
                            qtr4Lbl = "";
                            break;
                        case 2:
                            qtr1Lbl = value + " - Q1";
                            qtr2Lbl = value + " - Q2";
                            qtr3Lbl = "";
                            qtr4Lbl = "";
                            break;
                        case 3:
                            qtr1Lbl = value + " - Q1";
                            qtr2Lbl = value + " - Q2";
                            qtr3Lbl = value + " - Q3";
                            qtr4Lbl = "";
                            break;
                        case 4:
                            qtr1Lbl = value + " - Q1";
                            qtr2Lbl = value + " - Q2";
                            qtr3Lbl = value + " - Q3";
                            qtr4Lbl = value + " - Q4";
                            break;
                        default:
                            qtr1Lbl = "";
                            qtr2Lbl = "";
                            qtr3Lbl = "";
                            qtr4Lbl = "";
                            break;

                    }
                }

                //qtr1Lbl = value + " - Q1";
                //qtr2Lbl = value + " - Q2";
                //qtr3Lbl = value + " - Q3";
                //qtr4Lbl = value + " - Q4";
            } 
        }



        public fiscalYearViewModel()
        {


        }
        public void setOneDate(string fiscalWeek)
        {
            if (allData == null) return;  //must place data first in xaml code

            if ((from p in allData where p.displayFiscalWeek == fiscalWeek select p).Count() == 0)
                return;

            oneWeek = (from p in allData where p.displayFiscalWeek == fiscalWeek select p).First();

            multiWeekVisibility = Visibility.Collapsed;
            oneWeekVisibility = Visibility.Visible;
        }


        public void setStartDate(string fiscalWeek)
        {
            if (allData == null) return;  //must place data first in xaml code

            if ((from p in allData where p.displayFiscalWeek == fiscalWeek select p).Count() == 0)
                return;

            startWeek = (from p in allData where p.displayFiscalWeek == fiscalWeek select p).First();
        }
        public void setEndDate(string fiscalWeek)
        {
            if (allData == null) return;  //must place data first in xaml code

            if ((from p in allData where p.displayFiscalWeek == fiscalWeek select p).Count() == 0)
                return;


            endWeek = (from p in allData where p.displayFiscalWeek == fiscalWeek select p).First();
        }

        public void setData(ObservableCollection<fiscalData> d)
        {
            allData = d;


            displayYrs = (from p in allData orderby p.fiscalYear select p.fiscalYear.ToString()).Distinct().ToList<string>();

            selectedYear = (from p in allData where p.calDate == DateTime.Today select p.fiscalYear.ToString()).First<string>();
        }

        public void setQuarterDates(string qtr)
        {

            int quarter = int.Parse(qtr.Substring(qtr.Length - 1, 1));

            var query = from p in allData where p.fiscalQuarter == quarter && p.fiscalYear == int.Parse(selectedYear) select p;

            fiscalData first = (from p in query select p).First();
            fiscalData end = (from p in query select p).Last();

            if ((from m in query
                 where m.fiscalPeriod == end.fiscalPeriod
                 select m).Count() < 14)
            {
                end = (from p in query where p.fiscalPeriod == end.fiscalPeriod - 1 select p).Last();
            }

            startWeek = new fiscalData(first.fiscalWeek, first.fiscalYear, first.fiscalWeek.ToString());
            endWeek = new fiscalData(end.fiscalWeek, end.fiscalYear, end.fiscalWeek.ToString());


        }

    }
}
