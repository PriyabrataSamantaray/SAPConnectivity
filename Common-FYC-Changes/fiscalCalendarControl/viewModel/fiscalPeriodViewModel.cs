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
using System.ServiceModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using fiscalCalendar.prmExternalsSvc;

namespace fiscalCalendar.viewModel
{
    public class fiscalPeriodViewModel : viewModelBase
    {
        private ObservableCollection<fiscalData> _allData;
        public ObservableCollection<fiscalData> allData { get { return _allData; } set { SetProperty(ref _allData, value, "allData"); } }

        private List<fiscalData> _data;
        public List<fiscalData> data { get { return _data; } set { SetProperty(ref _data, value, "data"); } }

        private string _result;
        public string result { get { return _result; } set { SetProperty(ref _result, value, "result"); } }



        public fiscalPeriodViewModel()
        {

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


            fiscalData ps = (from p in allData
                    where p.calDate == DateTime.Today
                             select p).First<fiscalData>();

                
            data = (from p in allData
                    where p.fiscalPeriod == ps.fiscalPeriod && p.fiscalYear == ps.fiscalYear
                    select p).ToList<fiscalData>();
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
