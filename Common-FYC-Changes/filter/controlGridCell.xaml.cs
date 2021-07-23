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
    public partial class controlGridCell : UserControl
    {
        public bool isON;
        private filterPage myParent;

        public controlGridCell(filterPage mp)
        {
            InitializeComponent();
            isON = false;
            this.myParent = mp;
            
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            toggle();
        }
        public void setCellText(string txt)
        {
            cellText.Text = txt;
        }
        public string getCellText()
        {
            return cellText.Text;
        }

        public void toggle() //called from toggle and on page load  (changes only colors not values)
        {
            
            if (isON)
            {
                isON = false;
                borderObj.Background = new SolidColorBrush(Colors.Gray);
            }
            else
            {

                isON = true;
                borderObj.Background = new SolidColorBrush(Colors.Yellow);
            }
        }
    }
}
