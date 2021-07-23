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
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;


namespace slControls
{
    public partial class jobPanel : UserControl
    {
        public jobPanel()
        {
            InitializeComponent();
        }



        public void showdata(ObservableObjectCollection data)
        {

           
            dataGrid1.ItemsSource = data;

            LayoutRoot.Visibility = System.Windows.Visibility.Visible;



            (this.Resources["sbOpenHeight"] as Storyboard).Begin();
            (this.Resources["sbOpenWidth"] as Storyboard).Begin();

            //(this.Resources["sbOpenWidth2"] as Storyboard).Begin();

        }



        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LayoutRoot.Visibility = System.Windows.Visibility.Collapsed;
        }


    }





}
