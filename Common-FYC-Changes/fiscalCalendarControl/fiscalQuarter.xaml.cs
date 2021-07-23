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
using System.ComponentModel;
using fiscalCalendar.viewModel;
using System.Collections.ObjectModel;
using fiscalCalendar.prmExternalsSvc;

namespace fiscalCalendar
{
    public partial class fiscalQuarter : UserControl
    {
        public fiscalQuarterViewModel model;

        public static readonly DependencyProperty dataProperty = DependencyProperty.Register("data", typeof(ObservableCollection<fiscalData>), typeof(fiscalQuarter),
        new PropertyMetadata((new PropertyChangedCallback(fiscalQuarter.dataChanged))));


        public ObservableCollection<fiscalData> data
        {
            get { return (ObservableCollection<fiscalData>)GetValue(dataProperty); }
            set { SetValue(dataProperty, value); }
        }

        private static void dataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            fiscalQuarter u = d as fiscalQuarter;

            u.model.setData((ObservableCollection<fiscalData>)e.NewValue);

        }


        public static readonly DependencyProperty resultProperty = DependencyProperty.Register("result", typeof(string), typeof(fiscalQuarter),
        new PropertyMetadata((new PropertyChangedCallback(fiscalQuarter.resultChanged))));


        public string result
        {
            get { return (string)GetValue(resultProperty); }
            set { SetValue(resultProperty, value); }
        }

        private static void resultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


        }

        public static readonly DependencyProperty endPointProperty = DependencyProperty.Register("endPoint", typeof(string), typeof(fiscalQuarter),
        new PropertyMetadata((new PropertyChangedCallback(fiscalQuarter.endPointChanged))));


        public string endPoint
        {
            get { return (string)GetValue(endPointProperty); }
            set { SetValue(endPointProperty, value); }
        }

        private static void endPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            fiscalQuarter u = d as fiscalQuarter;

            u.model.setEndPoint(e.NewValue.ToString());

        }



        public fiscalQuarter()
        {
            InitializeComponent();
            model = (fiscalQuarterViewModel)Resources["ViewModel"];
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            result = c.SelectedValue.ToString();
        }
    }
}
