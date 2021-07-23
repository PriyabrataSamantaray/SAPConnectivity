﻿using System;
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
    public partial class deleteReport : ChildWindow
    {
        private iDeleteReport parent;


        public deleteReport(iDeleteReport _parent)
        {
            InitializeComponent();
            parent = _parent;
            if (parent.SaveNames == null)
                parent.SaveNames = new List<string>();



            comboBox1.ItemsSource = parent.SaveNames;

            if (parent.SaveNames.Count() > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            if (comboBox1.Items.Count > 0)
                parent.SelectedName = comboBox1.SelectedValue.ToString();
            else
                parent.SelectedName = null;
            parent.deleteReport(this, new EventArgs());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

