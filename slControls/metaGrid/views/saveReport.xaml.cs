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
    public partial class saveReport : ChildWindow
    {
        public string saveName { get; set; }
        private iSaveReport parent;

        public saveReport(iSaveReport _parent)
        {
            InitializeComponent();
            parent = _parent;

            if (parent.SaveNames == null)
                parent.SaveNames = new List<string>();



            editDrop1.dataSet = parent.SaveNames;
        }

        void saveReport_GotFocus(object sender, RoutedEventArgs e)
        {
            GotFocus -= new RoutedEventHandler(saveReport_GotFocus);
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            editDrop1.Focus();
          
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            GotFocus += new RoutedEventHandler(saveReport_GotFocus);

            parent.SelectedName = editDrop1.getText();
            parent.saveReport(this, new EventArgs());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           
            
            this.DialogResult = false;
            GotFocus += new RoutedEventHandler(saveReport_GotFocus);
        }





    }
}

