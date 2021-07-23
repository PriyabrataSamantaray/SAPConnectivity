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
using System.Windows.Browser;



namespace filter
{
    public delegate void MyDelegate();
    public delegate void mySetDelegate(List<string> arg, bool on);

    public partial class filterPage : UserControl
    {
        private bool allOn;
        private controlGridCell[] gc;
        private int rc;
        private MyDelegate cback;
        private mySetDelegate cbackInfo;


        public filterPage()
        {
            InitializeComponent();
        }
        public void setFilterArrayCallBack(mySetDelegate processArray)
        {
            cbackInfo = processArray;
        }
        private List<string> setFilterArray()
        {
            List<string>  filteredArrayData = new List<string>();

            for (int j = 0; j < gc.Length; j++)
            {
                if (gc[j].isON)
                    filteredArrayData.Add(gc[j].getCellText());
            }
            return filteredArrayData;
        }
        public void setWidth(double width)
        {
            GridLength gl = new GridLength(width);
            myCanvas.Width = width;
            myScroller.Width = width;
            myTable.Width = width - 20;


            Canvas.SetLeft(imgClose, width - 20);

        }

        public void setCallBack(MyDelegate del)
        {
            cback = del;
        }


        public void createTable(string[] data, List<string> filteredData)
        {
            rc = data.Length;

            List<string> filteredArrayData = new List<string>();

            if (filteredData.Count > 0)  //if nothing filtered use all data
                filteredArrayData = filteredData;
            else
                filteredArrayData = data.ToList();


            myTable.ColumnDefinitions.Clear();  //clear grid
            myTable.RowDefinitions.Clear();
            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            myTable.ColumnDefinitions.Add(colDef1);

            gc = new controlGridCell[rc];

            for (int i = 0; i < rc; i++)  //adds data to filter object
            {
                RowDefinition rowDef = new RowDefinition();
                GridLength gl = new GridLength(40);
                rowDef.Height = gl;
                myTable.RowDefinitions.Add(rowDef);
                gc[i] = new controlGridCell(this);
                gc[i].setCellText(data[i]);
                Grid.SetRow(gc[i], i);
                myTable.Children.Add(gc[i]);
            }

            for (int i = 0; i < filteredArrayData.Count; i++)  //turns yellow selected items
            {
                for (int j = 0; j < rc; j++)
                {
                    if (filteredArrayData[i] == gc[j].getCellText())
                    {
                        gc[j].toggle();
                        break;
                    }
                }
            }




        }
        private bool getImgState()
        {
            bool state = false;
            if (rc == setFilterArray().Count)
                state = true;
            else if (setFilterArray().Count == 0)
                state = true;
            else
                state = false;

            return state;
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Filter being applied
             */



            cbackInfo(setFilterArray(), getImgState());  //sets filtered array on parent through callback
            cback();  //call to perform actual filter
            this.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void closeDiv(object sender, MouseButtonEventArgs e)
        {
            cbackInfo(setFilterArray(), getImgState());  //sets filtered array on parent through callback
            this.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void chgFilter(object sender, MouseButtonEventArgs e)
        {
            string imgStr;



            for (int i = 0; i < rc; i++)
            {
                gc[i].isON = allOn;
                gc[i].toggle();
            }


            if(allOn)
            {
                imgStr = "allOff.jpg";
                ToolTipService.SetToolTip(filterImg,"All Off");
                allOn = false;
            }
            else
            {
                imgStr = "allOn.jpg";
                ToolTipService.SetToolTip(filterImg, "All On");
                allOn = true;
            }




            Uri myUri = new Uri(imgStr, UriKind.Relative);
            ImageSource img = new System.Windows.Media.Imaging.BitmapImage(myUri);
            filterImg.SetValue(Image.SourceProperty, img);

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



    }
}
