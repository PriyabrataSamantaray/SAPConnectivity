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
using fiscalCalendar.prmExternalsSvc;
using System.Collections.ObjectModel;


namespace fiscalCalendar
{
    public partial class calendar : UserControl
    {
        public calendarViewModel model;

        public static readonly DependencyProperty resultProperty = DependencyProperty.Register("result", typeof(string), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.resultChanged))));


        public string result
        {
            get { return (string)GetValue(resultProperty); }
            set { SetValue(resultProperty, value); }
        }

        private static void resultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {


        }

        public static readonly DependencyProperty startWeekProperty = DependencyProperty.Register("startWeek", typeof(fiscalData), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.startWeekChanged))));

        public static readonly DependencyProperty oneWeekProperty = DependencyProperty.Register("oneWeek", typeof(fiscalData), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.oneWeekChanged))));


        public fiscalData startWeek
        {
            get { return (fiscalData)GetValue(startWeekProperty); }
            set { SetValue(startWeekProperty, value); }
        }

        public fiscalData oneWeek
        {
            get { return (fiscalData)GetValue(oneWeekProperty); }
            set { SetValue(oneWeekProperty, value); }
        }


        private static void startWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            calendar u = d as calendar;
            u.model.setNewStartWeek((fiscalData)e.NewValue);
        }

        private static void oneWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            calendar u = d as calendar;
            u.model.setNewOneWeek((fiscalData)e.NewValue);
        }


        public static readonly DependencyProperty endWeekProperty = DependencyProperty.Register("endWeek", typeof(fiscalData), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.endWeekChanged))));


        public fiscalData endWeek
        {
            get { return (fiscalData)GetValue(endWeekProperty); }
            set { SetValue(endWeekProperty, value); }
        }

        private static void endWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            calendar u = d as calendar;
            u.model.setNewEndWeek((fiscalData)e.NewValue);


        }



        public static readonly DependencyProperty editableProperty = DependencyProperty.Register("editable", typeof(bool), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.editableChanged))));


        public bool editable
        {
            get { return (bool)GetValue(editableProperty); }
            set { SetValue(editableProperty, value); }
        }

        private static void editableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            calendar u = d as calendar;

            u.model.isEditable = (bool)e.NewValue;

        }



        public static readonly DependencyProperty dataProperty = DependencyProperty.Register("data", typeof(ObservableCollection<fiscalData>), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.dataChanged))));

        public static readonly DependencyProperty smallDataProperty = DependencyProperty.Register("smallData", typeof(List<fiscalData>), typeof(calendar),
        new PropertyMetadata((new PropertyChangedCallback(calendar.smallDataChanged))));


        public ObservableCollection<periodStructure> data
        {
            get { return (ObservableCollection<periodStructure>)GetValue(dataProperty); }
            set { SetValue(dataProperty, value); }
        }

        public List<fiscalData> smallData
        {
            get { return (List<fiscalData>)GetValue(smallDataProperty); }
            set { SetValue(smallDataProperty, value); }
        }

        private static void dataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            calendar u = d as calendar;

            u.model.setData((ObservableCollection<fiscalData>)e.NewValue);

        }
        private static void smallDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            calendar u = d as calendar;

            u.model.setSmallData((List<fiscalData>)e.NewValue);


        }



        public calendar()
        {
            InitializeComponent();
            model = (calendarViewModel)Resources["ViewModel"];

            model.PropertyChanged += new PropertyChangedEventHandler(model_PropertyChanged);
        }

        void model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "startWeek")
                startWeek = model.startWeek;
            if (e.PropertyName == "endWeek")
                endWeek = model.endWeek;
            if (e.PropertyName == "oneWeek")
                oneWeek = model.oneWeek;

        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (model.isEditable == false)
                return;

            StackPanel sp = (StackPanel)sender;

            model.displayWeekBkgnd(sp.Name);

        }




        private void nxtPeriod_Click(object sender, RoutedEventArgs e)
        {
            model.moveNextPeriod(true);
        }

        private void prvPeriod_Click(object sender, RoutedEventArgs e)
        {
            model.moveNextPeriod(false);
        }

        private void week_MouseLeave(object sender, MouseEventArgs e)
        {

            model.clearWeekBkgnd();
        }

        private void month_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (model.isEditable == false)
                return;

            model.setStartandEndWeek();
            
        }



        private void week_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (model.isEditable == false)
                return;

            StackPanel sp = (StackPanel)sender;
            model.setWeekBkgnd(sp.Name);
            result = model.getWeekLabel(sp.Name);
        }

        private void week_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (startWeek == null)
            {
                model.clearAll();
                result = "";
            }
            
        }



    }
}
