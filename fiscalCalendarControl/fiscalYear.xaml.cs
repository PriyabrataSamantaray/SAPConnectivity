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
    public partial class fiscalYear : UserControl
    {
        public fiscalYearViewModel model;


        public static readonly DependencyProperty startWeekProperty = DependencyProperty.Register("startWeek", typeof(string), typeof(fiscalYear),
        new PropertyMetadata((new PropertyChangedCallback(fiscalYear.startWeekChanged))));


        public static readonly DependencyProperty oneWeekProperty = DependencyProperty.Register("oneWeek", typeof(string), typeof(fiscalYear),
        new PropertyMetadata((new PropertyChangedCallback(fiscalYear.oneWeekChanged))));

        public string startWeek
        {
            get { return (string)GetValue(startWeekProperty); }
            set { SetValue(startWeekProperty, value); }
        }

        public string oneWeek
        {
            get { return (string)GetValue(oneWeekProperty); }
            set { SetValue(oneWeekProperty, value); }
        }

        private static void oneWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            fiscalYear u = d as fiscalYear;
            u.model.setOneDate(e.NewValue.ToString());


        }

        private static void startWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            fiscalYear u = d as fiscalYear;
            u.model.setStartDate(e.NewValue.ToString());


        }
        public static readonly DependencyProperty endWeekProperty = DependencyProperty.Register("endWeek", typeof(string), typeof(fiscalYear),
        new PropertyMetadata((new PropertyChangedCallback(fiscalYear.endWeekChanged))));


        public string endWeek
        {
            get { return (string)GetValue(endWeekProperty); }
            set { SetValue(endWeekProperty, value); }
        }

        private static void endWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            fiscalYear u = d as fiscalYear;
            u.model.setEndDate(e.NewValue.ToString());

        }


        public static readonly DependencyProperty dataProperty = DependencyProperty.Register("data", typeof(ObservableCollection<fiscalData>), typeof(fiscalYear),
        new PropertyMetadata((new PropertyChangedCallback(fiscalYear.dataChanged))));


        public ObservableCollection<fiscalData> data
        {
            get { return (ObservableCollection<fiscalData>)GetValue(dataProperty); }
            set { SetValue(dataProperty, value); }
        }

        private static void dataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            fiscalYear u = d as fiscalYear;

            u.model.setData((ObservableCollection<fiscalData>)e.NewValue);

        }

        private void quarter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label x = (Label)sender;

            model.setQuarterDates(x.Content.ToString());

        }


        public fiscalYear()
        {
            InitializeComponent();

            model = (fiscalYearViewModel)Resources["ViewModel"];
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_PropertyChanged);
        }

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "startWeek")
                startWeek = model.startWeek.displayFiscalWeek;
            if (e.PropertyName == "endWeek")
                endWeek = model.endWeek.displayFiscalWeek;
            if (e.PropertyName == "oneWeek")
                oneWeek = model.oneWeek.displayFiscalWeek;
        }




    }
}
