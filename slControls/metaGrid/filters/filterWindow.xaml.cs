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

namespace slControls.metaGrid.filters
{
    public partial class filterWindow : ChildWindow
    {
        private bool allOn;

        private controlGridCell[] gc;
        private int rc;
        private iFilterWindow parent;
        public List<string> filteredArrayData;
        public string bindingName;
        public string imgStr = "Filter-Off.png";

        public filterWindow(iFilterWindow _parent)
        {
            InitializeComponent();
            parent = _parent;
            allOn = false;
        }
        #region styles
        private void hover_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Image tmpImg = (System.Windows.Controls.Image)sender;
            tmpImg.Cursor = Cursors.Hand;
        }

        private void hover_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Image tmpImg = (System.Windows.Controls.Image)sender;

            tmpImg.Cursor = Cursors.Arrow;
        }

        private void button1_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Button tmpBut = (System.Windows.Controls.Button)sender;
            tmpBut.Cursor = Cursors.Hand;
        }

        private void button1_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Button tmpBut = (System.Windows.Controls.Button)sender;
            tmpBut.Cursor = Cursors.Arrow;
        }
        #endregion
        private void chgFilter(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < rc; i++)
            {
                gc[i].isON = allOn;
                gc[i].toggle();
            }
            allOn = !allOn;
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            filteredArrayData = new List<string>();

            for (int j = 0; j < gc.Length; j++)
            {
                if (gc[j].isON)
                    filteredArrayData.Add(gc[j].getCellText());
            }

            if(filteredArrayData.Count == rc)
                filteredArrayData = new List<string>();
            
            this.DialogResult = true;

            imgStr = "Filter-Off.png";
            if (filteredArrayData.Count() > 0)
                imgStr = "Filter-On.png";

            setfilterImage(imgStr);



            parent.filterGrid(this, new EventArgs());

        }

        private void setfilterImage(string imgStr)
        {
            Uri myUri = new Uri(@"img/" + imgStr, UriKind.Relative);
            ImageSource img = new System.Windows.Media.Imaging.BitmapImage(myUri);
            filterImg.SetValue(Image.SourceProperty, img);
        }

        public List<string> convertToColors(List<string> values)
        {
            List<string> results = new List<string>();

            foreach (string s in values)
            {

                if (s == "1.00")
                    results.Add("green");
                else if (s == "0")
                    results.Add("gray");
                else if (s == "")
                    results.Add("blank");
                else if (decimal.Parse(s) > 1)
                    results.Add("red");
                else
                    results.Add("yellow");


            }

            return (from p in results select p).Distinct<string>().ToList<string>();

        }


        public void setGraphList(List<string> fullList, List<string> selected, string bindingName)
        {

            this.bindingName = bindingName;

            myTable.ColumnDefinitions.Clear();  //clear grid
            myTable.RowDefinitions.Clear();
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            myTable.ColumnDefinitions.Add(colDef1);



            fullList = convertToColors(fullList);

            rc = fullList.Count();

            gc = new controlGridCell[rc];
            int i = 0;

            if (selected.Count() == 0)
            {
                allOn = true;
                selected = fullList;
            }

            foreach (string s in fullList)
            {
                RowDefinition rowDef = new RowDefinition();
                GridLength gl = new GridLength(40);
                rowDef.Height = gl;
                myTable.RowDefinitions.Add(rowDef);
                gc[i] = new controlGridCell(true);
                gc[i].setCellText(s);
                Grid.SetRow(gc[i], i);
                myTable.Children.Add(gc[i]);
                if (selected.Contains(s)) //paint selected yellow
                    gc[i].toggle();
                i++;
            }



        }

        public void setList(List<string> fullList, List<string> selected, string bindingName)
        {
            rc = fullList.Count();
            this.bindingName = bindingName;

            myTable.ColumnDefinitions.Clear();  //clear grid
            myTable.RowDefinitions.Clear();
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            myTable.ColumnDefinitions.Add(colDef1);

            gc = new controlGridCell[rc];
            int i = 0;


            if (selected.Count() == 0)
            {
                allOn = true;
                selected = fullList;
            }

            foreach(string s in fullList)
            {
                RowDefinition rowDef = new RowDefinition();
                GridLength gl = new GridLength(40);
                rowDef.Height = gl;
                myTable.RowDefinitions.Add(rowDef);
                gc[i] = new controlGridCell(false);
                gc[i].setCellText(s);
                Grid.SetRow(gc[i], i);
                myTable.Children.Add(gc[i]);
                if (selected.Contains(s)) //paint selected yellow
                    gc[i].toggle();
                i++;
            }



        }
    }
}

