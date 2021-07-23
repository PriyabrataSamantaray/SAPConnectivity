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
    public partial class shadeButton : UserControl
    {
        public bool appOpen { get; set; }

        //public static readonly DependencyProperty stateProperty = DependencyProperty.Register("state", typeof(bool), typeof(shadeButton),
        //new PropertyMetadata((new PropertyChangedCallback(shadeButton.stateValueChanged))));


        //private static void stateValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    return;

        //}

        //public bool state
        //{
        //    get { return (bool)GetValue(appOpen); }
        //    set { SetValue(idProperty, value); }
        //}



        public shadeButton()
        {
            InitializeComponent();
            appOpen = true;
        }

        private void LayoutRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            if (appOpen)
            {
                closeBig.Visibility = System.Windows.Visibility.Visible;
                closeSmall.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                openBig.Visibility = System.Windows.Visibility.Visible;
                openSmall.Visibility = System.Windows.Visibility.Collapsed;
            }

        }
        public void forceClose()
        {
            appOpen = false;
            closeBig.Visibility = System.Windows.Visibility.Collapsed;
            closeSmall.Visibility = System.Windows.Visibility.Collapsed;
            openBig.Visibility = System.Windows.Visibility.Collapsed;
            openSmall.Visibility = System.Windows.Visibility.Visible;
        }

        private void LayoutRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            if (appOpen)
            {
                closeBig.Visibility = System.Windows.Visibility.Collapsed;
                closeSmall.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                openBig.Visibility = System.Windows.Visibility.Collapsed;
                openSmall.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void closeBig_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appOpen = false;
            closeBig.Visibility = System.Windows.Visibility.Collapsed;
            openBig.Visibility = System.Windows.Visibility.Visible;
        }

        private void openBig_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            appOpen = true;
            closeBig.Visibility = System.Windows.Visibility.Visible;
            openBig.Visibility = System.Windows.Visibility.Collapsed;
        }



    }
}
