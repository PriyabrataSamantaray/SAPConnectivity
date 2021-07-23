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

namespace slControls
{
    public partial class utilizer : UserControl
    {

        public static readonly DependencyProperty idProperty = DependencyProperty.Register("id", typeof(string), typeof(utilizer),
        new PropertyMetadata((new PropertyChangedCallback(utilizer.idValueChanged))));

        public static readonly DependencyProperty periodProperty = DependencyProperty.Register("period", typeof(int), typeof(utilizer),
        new PropertyMetadata((new PropertyChangedCallback(utilizer.periodValueChanged))));

        public static readonly DependencyProperty valueStrProperty = DependencyProperty.Register("valueStr", typeof(string), typeof(utilizer),
        new PropertyMetadata((new PropertyChangedCallback(utilizer.valueStrValueChanged))));

        public static readonly DependencyProperty valueDecProperty = DependencyProperty.Register("valueDec", typeof(decimal), typeof(utilizer),
        new PropertyMetadata((new PropertyChangedCallback(utilizer.valueDecValueChanged))));

        public static readonly DependencyProperty subtotalProperty = DependencyProperty.Register("subtotal", typeof(string), typeof(utilizer),
        new PropertyMetadata((new PropertyChangedCallback(utilizer.subTotalValueChanged))));


        public string id
        {
            get { return (string)GetValue(idProperty); }
            set { SetValue(idProperty, value); }
        }
        public string valueStr
        {
            get { return (string)GetValue(valueStrProperty); }
            set { SetValue(valueStrProperty, value); }
        }
        public decimal valueDec
        {
            get { return (decimal)GetValue(valueDecProperty); }
            set { SetValue(valueDecProperty, value); }
        }
        public string subtotal
        {
            get { return (string)GetValue(subtotalProperty); }
            set { SetValue(subtotalProperty, value); }
        }
        public int period
        {
            get { return (int)GetValue(periodProperty); }
            set { SetValue(periodProperty, value); }
        }

        public utilizer()
        {
            InitializeComponent();

            
        }

        private static void subTotalValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            if (e.NewValue == null)
                return;

             decimal val = decimal.Parse(e.NewValue.ToString());

            if (val < 0)
                return;

            utilizer u = d as utilizer;
            u.redBar.Visibility = Visibility.Collapsed;
            u.yellowBar.Visibility = Visibility.Collapsed;
            u.greenBar.Visibility = Visibility.Collapsed;
            u.grayBar.Visibility = Visibility.Collapsed;
            u.label1.Visibility = Visibility.Collapsed;
           

            u.label2.Content = val.ToString();
            u.label2.Visibility = Visibility.Visible;

        }
        private static void valueDecValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double maxWidth = 40;  //bar width

            if (e.NewValue == null)
                return;

            //if (decimal.Parse( e.NewValue.ToString()) == 0)
            //    return;
            utilizer u = d as utilizer;
            u.redBar.Visibility = Visibility.Collapsed;
            u.yellowBar.Visibility = Visibility.Collapsed;
            u.greenBar.Visibility = Visibility.Collapsed;
            u.grayBar.Visibility = Visibility.Collapsed;
            u.label1.Visibility = Visibility.Visible;
            u.label2.Visibility = Visibility.Collapsed;

            decimal hiValue = decimal.Parse(e.NewValue.ToString());


            if (hiValue == 1)
            {
                u.greenBar.Visibility = Visibility.Visible;
            }
            else if (hiValue < 1)
            {
                u.yellowBar.Visibility = Visibility.Visible;
                u.grayBar.Visibility = Visibility.Visible;


                u.yellowBar.Margin = new Thickness(maxWidth, 0, maxWidth - (double)(hiValue * (decimal)maxWidth), 0);
                //u.yellowBar.Width = (double) (hiValue * maxWidth);
            }
            else
            {
                u.redBar.Visibility = Visibility.Visible;
            }


            u.label1.Content = ((int)(hiValue * 100)).ToString() + "%";
        }
        private static void valueStrValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double maxWidth = 40;  //bar width

            if (e.NewValue == null )
                return;

            if (e.NewValue.ToString() == "")
                return;

            utilizer u = d as utilizer;
            u.redBar.Visibility = Visibility.Collapsed;
            u.yellowBar.Visibility = Visibility.Collapsed;
            u.greenBar.Visibility = Visibility.Collapsed;
            u.grayBar.Visibility = Visibility.Collapsed;
            u.label1.Visibility = Visibility.Visible;
            u.label2.Visibility = Visibility.Collapsed;

            decimal hiValue = decimal.Parse(e.NewValue.ToString());


            if (hiValue == 1)
            {
                u.greenBar.Visibility = Visibility.Visible;
            }
            else if (hiValue < 1)
            {
                u.yellowBar.Visibility = Visibility.Visible;
                u.grayBar.Visibility = Visibility.Visible;


                u.yellowBar.Margin = new Thickness(maxWidth, 0, maxWidth - (double)(hiValue * (decimal)maxWidth), 0);
                //u.yellowBar.Width = (double) (hiValue * maxWidth);
            }
            else
            {
                u.redBar.Visibility = Visibility.Visible;
            }


            u.label1.Content = ((int)(hiValue * 100)).ToString() + "%";
        }



        private static void idValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            utilizer u = d as utilizer;

            if (e.NewValue == null)//move from integer to string caused nullable to be an option
            {
                u.LayoutRoot.Visibility = Visibility.Collapsed;
                return;
            }
            if (e.NewValue.ToString() != "0" && e.NewValue.ToString() != "")
                u.LayoutRoot.Visibility = Visibility.Visible;
            else
                u.LayoutRoot.Visibility = Visibility.Collapsed;

        }
        private static void periodValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }








    }
}
