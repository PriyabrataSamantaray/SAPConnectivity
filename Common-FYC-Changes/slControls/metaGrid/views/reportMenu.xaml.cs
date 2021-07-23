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
using slControls.metaGrid.interfaces;

namespace slControls.metaGrid
{
    public partial class reportMenu : ChildWindow
    {
        private iReportmenu parent;

        public reportMenu(iReportmenu _parent)
        {
            InitializeComponent();
            parent = _parent;

        }
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }

        private void image2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;



        }

        private void image5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;


        }


        private void image3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.DialogResult = true;



        }

        private void image4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;


        }

        private void showHide_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            parent.rMenu_showColumns(this, new EventArgs());


        }

        private void saveReport_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            parent.rMenu_saveClicked(this, new EventArgs());
        }

        private void deleteReport_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            parent.rMenu_deleteClicked(this, new EventArgs());
        }

        private void exportReport_Click(object sender, RoutedEventArgs e)
        {
            parent.rMenu_exportClicked(this, new EventArgs());
            this.DialogResult = true;
        }

        private void exportReport_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }




    }
}

