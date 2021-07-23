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


namespace slControls.metaGrid.filters
{
    public partial class controlGridCell : UserControl
    {
        public bool isON;
        private bool graphical;
        private commonFuncs cfunc;
        private string actualText;

        public controlGridCell( bool g)
        {
            InitializeComponent();
            isON = false;
            graphical = g;
            cfunc = new commonFuncs();


        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            toggle();

        }
        public void setCellText(string txt)
        {
            this.actualText = txt;
            if (graphical)
            {
                switch (txt)
                {
                    case "red":
                        graphicText.Fill = new SolidColorBrush(cfunc.getRGBColor("FFE52323"));
                        cellText.Text = "Over Allocated";
                        break;
                    case "yellow":
                        graphicText.Fill = new SolidColorBrush(Colors.Yellow);
                        cellText.Text = "Under Allocated";
                        break;
                    case "green":
                        graphicText.Fill = new SolidColorBrush(Colors.Green);
                        cellText.Text = "Level Allocated";
                        break;
                    case "gray":
                        graphicText.Fill = new SolidColorBrush(Colors.Gray);
                        cellText.Text = "Not Allocated";
                        break;
                    case "blank":
                        cellText.Text = "Blank";
                        break;

                }
                if (txt != "blank")
                    graphicText.Visibility = System.Windows.Visibility.Visible;

            }
            else
                cellText.Text = txt;
        }
        public string getCellText()
        {
            return actualText;
        }

        public void toggle() //called from toggle and on page load  (changes only colors not values)
        {

            if (isON)
            {
                isON = false;
                borderObj.Background = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                isON = true;
                borderObj.Background = new SolidColorBrush(cfunc.getRGBColor("FFF2F263"));
            }



        }
    }
}
