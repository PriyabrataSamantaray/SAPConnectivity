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
    public class flipPanel : UserControl
    {
        private Storyboard sb1 = new Storyboard();
        private Storyboard sb2 = new Storyboard();
        private Storyboard sb3 = new Storyboard();
        PlaneProjection plane1 = new PlaneProjection();
        PlaneProjection plane2 = new PlaneProjection();
        private string planeName1;
        private string planeName2;
        private PropertyPath rotProp;
        private bool hasFlipped = false;

        public flipPanel(UserControl uc, Border front, Border back, bool isHorizontal)
        {
            setPlanes(front.Name, back.Name, isHorizontal);
            back.Margin = front.Margin;
            back.Visibility = System.Windows.Visibility.Visible;
            front.Projection = plane1;
            back.Projection = plane2;
            setStories(uc, front.Name, back.Name);
           
        }
        public flipPanel(UserControl uc, Grid front, Grid back, bool isHorizontal)
        {
            setPlanes(front.Name, back.Name, isHorizontal);
            back.Margin = front.Margin;
            back.Visibility = System.Windows.Visibility.Visible;
            front.Projection = plane1;
            back.Projection = plane2;
            setStories(uc, front.Name, back.Name);
        }
        public flipPanel(UserControl uc, Canvas front, Canvas back, bool isHorizontal)
        {
            setPlanes(front.Name, back.Name, isHorizontal);
            back.Margin = front.Margin;
            back.Visibility = System.Windows.Visibility.Visible;
            front.Projection = plane1;
            back.Projection = plane2;
            setStories(uc, front.Name, back.Name);
        }
        public void callFlip()
        {
            if (hasFlipped)
            {
                hasFlipped = false;
                sb2.Begin();
            }
            else
            {
                hasFlipped = true;
                sb1.Begin();
            }

        }
        public void callPassFlip()
        {
            sb1.Completed += new EventHandler(sb1_Completed);
            sb1.Begin();
        }

        void sb1_Completed(object sender, EventArgs e)
        {
            sb1.Completed -= new EventHandler(sb1_Completed);
            sb3.Begin();
        }

        private void setStories(UserControl uc, string frontName, string backName)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.To = 90;
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName1);
            sb1.Children.Add(da);
            da = new DoubleAnimation();
            da.To = 0;
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.BeginTime = new TimeSpan(0, 0, 0, 0, 500);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName2);
            sb1.Children.Add(da); //create storyboard 1

            da = new DoubleAnimation();
            da.To = -90;
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName2);
            sb2.Children.Add(da);
            da = new DoubleAnimation();
            da.To = 0;
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.BeginTime = new TimeSpan(0, 0, 0, 0, 500);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName1);
            sb2.Children.Add(da);

            da = new DoubleAnimation();
            da.To = -90;
            da.BeginTime = new TimeSpan(0, 0, 0, 0, 500);
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName2);
            sb3.Children.Add(da);
            da = new DoubleAnimation();
            da.To = 0;
            da.Duration = new TimeSpan(0, 0, 0, 0, 500);
            da.BeginTime = new TimeSpan(0, 0, 0, 1);
            da.SetValue(Storyboard.TargetPropertyProperty, rotProp);
            da.SetValue(Storyboard.TargetNameProperty, planeName1);
            sb3.Children.Add(da);

            //Add story Boards to user Controls
            uc.Resources.Add("sb-" + frontName, sb1);
            uc.Resources.Add("sb-" + backName, sb2);
            uc.Resources.Add("sb-x" + backName, sb3);
        }


        private void setPlanes(string frontName, string backName, bool isHorizontal)
        {
            planeName1 = "pl-" + frontName;
            planeName2 = "pl-" + backName;

            plane1.SetValue(NameProperty, planeName1);
            plane2.SetValue(NameProperty, planeName2);
            if (isHorizontal)
            {
                plane2.RotationY = -90;
                rotProp = new PropertyPath("(RotationY)");
            }
            else
            {
                plane2.RotationX = -90;
                rotProp = new PropertyPath("(RotationX)");
            }
        }


    }
}
