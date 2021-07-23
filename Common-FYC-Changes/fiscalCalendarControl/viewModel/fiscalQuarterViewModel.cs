using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using fiscalCalendar.prmExternalsSvc;
using System.Windows.Shapes;
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace fiscalCalendar.viewModel
{
    public class fiscalQuarterViewModel : viewModelBase
    {
        private ObservableCollection<fiscalData> allData;

        private List<fiscalData> _per1;
        public List<fiscalData> per1 { get { return _per1; } set { SetProperty(ref _per1, value, "per1"); } }
        private List<fiscalData> _per2;
        public List<fiscalData> per2 { get { return _per2; } set { SetProperty(ref _per2, value, "per2"); } }
        private List<fiscalData> _per3;
        public List<fiscalData> per3 { get { return _per3; } set { SetProperty(ref _per3, value, "per3"); } }

        private List<string> _displayQtrs;
        public List<string> displayQtrs { get { return _displayQtrs; } set { SetProperty(ref _displayQtrs, value, "displayQtrs"); } }

        private string _selectedQtr;
        public string selectedQtr
        {
            get
            {
                return _selectedQtr;
            }
            set
            {
                _selectedQtr = value;
                OnPropertyChanged("selectedQtr");

                int month1, month2, month3, year;

                List<fiscalData> d = (from p in allData where p.displayQtr == selectedQtr select p).ToList<fiscalData>();


                month1 = (from p in d  select p.fiscalPeriod).Min();
                year = (from p in d select p.fiscalYear).Min();
                month3 = (from p in d select p.fiscalPeriod).Max();
                month2 = (from p in d where p.fiscalPeriod != month1 && p.fiscalPeriod != month3 select p.fiscalPeriod).First();


                per1 = (from p in d
                        where p.fiscalPeriod == month1 && p.fiscalYear == year
                        select p).ToList<fiscalData>();

                per2 = (from p in d
                        where p.fiscalPeriod == month2 && p.fiscalYear == year
                        select p).ToList<fiscalData>();

                per3 = (from p in d
                        where p.fiscalPeriod == month3 && p.fiscalYear == year
                        select p).ToList<fiscalData>();
            }

        }


        public fiscalQuarterViewModel()
        {
            if (!IsDesignTime)
            {
                //System.ServiceModel.BasicHttpBinding b = new BasicHttpBinding();
                //b.MaxReceivedMessageSize = 2147483647;
                //prmExternalsSoapClient prmClient = new prmExternalsSoapClient(b, new EndpointAddress(@"http://localhost:2000/rfcWebServices/prmExternals.asmx"));





            }
        }

        public void setEndPoint(string point)
        {
            prmExternalsSoapClient prmClient;

            if (point == "")
                prmClient = new prmExternalsSoapClient();
            else
            {
                System.ServiceModel.BasicHttpBinding b = new BasicHttpBinding();
                b.MaxReceivedMessageSize = 2147483647;
                prmClient = new prmExternalsSoapClient(b, new EndpointAddress(point));

            }


            prmClient.fiscalPeriodsCompleted += new EventHandler<fiscalPeriodsCompletedEventArgs>(prmClient_fiscalPeriodsCompleted);
            prmClient.fiscalPeriodsAsync();

        }

        public void setData(ObservableCollection<fiscalData> d)
        {
            allData = d;


            displayQtrs = (from p in allData
                           orderby p.fiscalYear, p.fiscalQuarter
                           select p.displayQtr).Distinct<string>().ToList<string>();

            selectedQtr = (from p in allData where p.calDate == DateTime.Today select p.displayQtr).First<string>();
        }


        void prmClient_fiscalPeriodsCompleted(object sender, fiscalPeriodsCompletedEventArgs e)
        {
            ObservableCollection<periodStructure> ans = e.Result;
            ObservableCollection<fiscalData> newData = new ObservableCollection<fiscalData>();

            foreach (periodStructure p in ans)
            {
                fiscalData d = new fiscalData(p.calDate, p.fiscalWeek, p.fiscalPeriod, p.fiscalQuarter, p.fiscalYear, p.workDay);
                newData.Add(d);

            }




            setData(newData);


        }


    }
}
