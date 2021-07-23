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
    public partial class filterHeader : UserControl
    {
        public bool hitImage;


        public static readonly DependencyProperty contentProperty = DependencyProperty.Register("content", typeof(string), typeof(filterHeader),
        new PropertyMetadata((new PropertyChangedCallback(filterHeader.contentValueChanged))));

        public static readonly DependencyProperty filterStateProperty = DependencyProperty.Register("filterState", typeof(bool), typeof(filterHeader),
        new PropertyMetadata((new PropertyChangedCallback(filterHeader.filterStateValueChanged))));



        public filterHeader()
        {
            InitializeComponent();

        }

        public string content
        {
            get { return (string)GetValue(contentProperty); }
            set { SetValue(contentProperty, value); }
        }

        public bool filterState
        {
            get { return (bool)GetValue(filterStateProperty); }
            set { SetValue(filterStateProperty, value); }
        }

        private static void contentValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            filterHeader u = d as filterHeader;
            u.label1.Content = e.NewValue.ToString();
        }
        private static void filterStateValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            filterHeader u = d as filterHeader;
            u.setFilterImg((bool) e.NewValue); 
        }


        public void setFilterImg(bool state)
        {
            string imgStr;


            if (state == false)
                imgStr = "Filter-Off.png";
            else
                imgStr = "Filter-On.png";


            Uri myUri = new Uri(@"img/" + imgStr, UriKind.Relative);
            ImageSource img = new System.Windows.Media.Imaging.BitmapImage(myUri);
            img1.SetValue(Image.SourceProperty, img);

        }

        private void img1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hitImage = true;
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hitImage = false;
        }



    }
}
