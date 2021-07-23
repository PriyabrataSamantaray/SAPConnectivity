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
using slControls;

namespace slControls
{
    public partial class qualLevel : UserControl
    {
        commonFuncs cfuncs = new commonFuncs();

        public static readonly DependencyProperty colorProperty = DependencyProperty.Register("color", typeof(string), typeof(qualLevel),
        new PropertyMetadata((new PropertyChangedCallback(qualLevel.colorChanged))));

        public string color
        {
            get { return (string)GetValue(colorProperty); }
            set { SetValue(colorProperty, value); }
        }

        private static void colorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            qualLevel q = d as qualLevel;

            q.LayoutRoot.Visibility = Visibility.Collapsed;


            if (e.NewValue == null)
                return;


            if (e.NewValue.ToString() == "blank")
                return;

            if (e.NewValue.ToString() == "green")
            {
                q.dot.Fill = new SolidColorBrush(q.cfuncs.getRGBColor("FF51FD10"));
                q.dot.Stroke = new SolidColorBrush(q.cfuncs.getRGBColor("FF51FD10"));  

            }
            else if (e.NewValue.ToString() == "red")
            {
                q.dot.Fill = new SolidColorBrush(q.cfuncs.getRGBColor("FFFD2610"));
                q.dot.Stroke = new SolidColorBrush(q.cfuncs.getRGBColor("FFFD2610"));

            }
            else if (e.NewValue.ToString() == "yellow")
            {
                q.dot.Fill = new SolidColorBrush(q.cfuncs.getRGBColor("FFF3EA1D"));
                q.dot.Stroke = new SolidColorBrush(q.cfuncs.getRGBColor("FFF3EA1D"));
            }
            else
            {
                return;
            }
            q.LayoutRoot.Visibility = Visibility.Visible;
        }



        public qualLevel()
        {
            InitializeComponent();
        }
    }
}
