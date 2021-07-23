using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using fiscalCalendar.viewModel;
using System.Collections.ObjectModel;
using fiscalCalendar.prmExternalsSvc;

namespace fiscalCalendar
{
    public partial class fiscalPeriod : UserControl
    {

        public fiscalPeriodViewModel model;


        public static readonly DependencyProperty resultProperty = DependencyProperty.Register("result", typeof(string), typeof(fiscalPeriod),
        new PropertyMetadata((new PropertyChangedCallback(fiscalPeriod.resultChanged))));


        public string result
        {
            get { return (string)GetValue(resultProperty); }
            set { SetValue(resultProperty, value); }
        }

        private static void resultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


        }






        public static readonly DependencyProperty dataProperty = DependencyProperty.Register("data", typeof(ObservableCollection<fiscalData>), typeof(fiscalPeriod),
        new PropertyMetadata((new PropertyChangedCallback(fiscalPeriod.dataChanged))));


        public ObservableCollection<fiscalData> data
        {
            get { return (ObservableCollection<fiscalData>)GetValue(dataProperty); }
            set { SetValue(dataProperty, value); }
        }

        private static void dataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            fiscalPeriod u = d as fiscalPeriod;

            u.model.setData((ObservableCollection<fiscalData>)e.NewValue);

        }




        public fiscalPeriod()
        {
            InitializeComponent();
            model = (fiscalPeriodViewModel)Resources["ViewModel"];
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_PropertyChanged);
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "result")
            {
                result = model.result;

            }
        }
    }
}
