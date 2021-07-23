using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace slControls
{
    public class easyTransition
    {
        private Storyboard sb1 = new Storyboard();
        private Storyboard sb2 = new Storyboard();
        private bool topisOn = true;
        private Grid front;
        private Grid back;
        private Grid frontCanvas;

        private PropertyPath colorProp;
        private UserControl uc;

        private commonFuncs cfuncs = new commonFuncs();


        public easyTransition(UserControl _uc, Grid _front, Grid _back, Grid _frontCanvas)
        {
            front = _front;
            back = _back;
            frontCanvas = _frontCanvas;

            uc = _uc;

            back.Margin = front.Margin;
            back.Visibility = Visibility.Collapsed;


            ColorAnimation ca = new ColorAnimation();
            colorProp = new PropertyPath("(Canvas.Background).(SolidColorBrush.Color)");
            ca.From = cfuncs.getRGBColor("FFA89D77");
            ca.To =  cfuncs.getRGBColor("FFDECB88"); 
            ca.Duration = new TimeSpan(0, 0, 0, 2, 500);

            ca.SetValue(Storyboard.TargetPropertyProperty, colorProp);
            ca.SetValue(Storyboard.TargetNameProperty, frontCanvas.Name);

            sb1.Children.Add(ca);

            ColorAnimation ca2 = new ColorAnimation();
            ca2.From = cfuncs.getRGBColor("FFDECB88");
            ca2.To = cfuncs.getRGBColor("FFA89D77");
            ca2.Duration = new TimeSpan(0, 0, 0, 2, 500);

            ca2.SetValue(Storyboard.TargetPropertyProperty, colorProp);
            ca2.SetValue(Storyboard.TargetNameProperty, frontCanvas.Name);


            sb2.Children.Add(ca2);


            //Add story Boards to user Controls
            uc.Resources.Add("sb-" + frontCanvas.Name, sb1);

            uc.Resources.Add("sb2-" + frontCanvas.Name, sb2);

        }


        public void transition()
        {




            if (topisOn)
            {
                front.Visibility = Visibility.Collapsed;
                back.Visibility = Visibility.Visible;
                sb2.Begin();
            }
            else
            {
                front.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Collapsed;
                sb1.Begin();
            }
            topisOn = !topisOn;
        }
    }
}
