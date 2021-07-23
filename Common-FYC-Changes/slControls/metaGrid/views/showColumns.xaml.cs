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
    public partial class showColumns : ChildWindow
    {
        public event EventHandler listReordered;

        private iShowColumns parent;
        private List<metaColumn> data;

        private List<metaColumn> shownData;
        private List<metaColumn> avalData;


        public string selectedShow { get; set; }


        public showColumns(iShowColumns _parent)
        {
            InitializeComponent();

            parent = _parent;
            data = _parent.metaData;

            shownData = (from p in data where p.isVisible == true && p.header != "" select p).ToList<metaColumn>();
            avalData = (from p in data where p.isVisible == false && p.header != "" orderby p.header select p).ToList<metaColumn>();

            initLists();
        }
        private void initLists()
        {


            shown.ItemsSource = (from p in shownData select p.header).ToList<string>();
            hidden.ItemsSource = (from p in avalData select p.header).ToList<string>();


            leftbutton.IsEnabled = false;
            rightbutton.IsEnabled = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            data = shownData.Concat(avalData).ToList<metaColumn>();

            parent.metaDataChanged(data);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void shown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox l = sender as ListBox;

            leftbutton.IsEnabled = true;
            upbutton.IsEnabled = true;
            downbutton.IsEnabled = true;

            l.ScrollIntoView(l.SelectedIndex); 
            l.UpdateLayout();
        }

        private void leftbutton_Click(object sender, RoutedEventArgs e)
        {
            metaColumn col = (from p in data where p.header == shown.SelectedItem.ToString() select p).First<metaColumn>();
            col.isVisible = false;

            shownData = (from p in data where p.isVisible == true && p.header != "" select p).ToList<metaColumn>();
            avalData = (from p in data where p.isVisible == false && p.header != "" orderby p.header select p).ToList<metaColumn>();


            initLists();

            upbutton.IsEnabled = false;
            downbutton.IsEnabled = false;

        }

        private void rightbutton_Click(object sender, RoutedEventArgs e)
        {
            metaColumn col = (from p in data where p.header == hidden.SelectedItem.ToString() select p).First<metaColumn>();
            avalData.Remove(col);
            col.isVisible = true;
            
            int idx = shown.SelectedIndex;
            if (idx > -1)
                shownData.Insert(idx, col);
            else
                shownData.Add(col);



            initLists();


           
        }

        private void hidden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rightbutton.IsEnabled = true;
        }

        private void upbutton_Click(object sender, RoutedEventArgs e)
        {
            metaColumn col = (from p in shownData where p.header == shown.SelectedItem.ToString() select p).First<metaColumn>();

            int idx = shownData.IndexOf(col);

            shownData.Remove(col);
            
            if (idx  == 0)
                shownData.Add(col);
            else
                shownData.Insert(idx - 1, col);

            initLists();

            if (idx == 0)
                idx = shown.Items.Count();

            shown.SelectedIndex = idx - 1;

        }

        private void downbutton_Click(object sender, RoutedEventArgs e)
        {
            metaColumn col = (from p in shownData where p.header == shown.SelectedItem.ToString() select p).First<metaColumn>();

            int idx = shownData.IndexOf(col);

            shownData.Remove(col);

            if (idx == shownData.Count()  )
                shownData.Insert(0, col);
            else
                shownData.Insert(idx + 1, col);

            initLists();

            if (idx + 1 == shownData.Count() )
                idx = -1;

            shown.SelectedIndex = idx + 1;
        }

        private void allrightbutton_Click(object sender, RoutedEventArgs e)
        {
            foreach (metaColumn col in data)
            {
                col.isVisible = false;
            }
            shownData = (from p in data where p.isVisible == true && p.header != "" select p).ToList<metaColumn>();
            avalData = (from p in data where p.isVisible == false && p.header != "" orderby p.header select p).ToList<metaColumn>();

            initLists();
        }

        private void allleftbutton_Click(object sender, RoutedEventArgs e)
        {
            foreach (metaColumn col in data)
            {
                col.isVisible = true;
            }
            shownData = (from p in data where p.isVisible == true && p.header != "" select p).ToList<metaColumn>();
            avalData = (from p in data where p.isVisible == false && p.header != "" orderby p.header select p).ToList<metaColumn>();

            initLists();
        }



    }
}

