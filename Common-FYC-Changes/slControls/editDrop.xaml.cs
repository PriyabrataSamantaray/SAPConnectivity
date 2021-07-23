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


namespace slControls
{
    public partial class editDrop : UserControl
    {
        public bool isNumeric;
        private Storyboard flip;
        private commonFuncs cf;

        public string selectedValue
        {
            get { return (string)GetValue(valueProperty); }
            set { SetValue(valueProperty, value); }

        }
        public static readonly DependencyProperty valueProperty = DependencyProperty.Register("selectedValue", typeof(string), typeof(editDrop),
        new PropertyMetadata((new PropertyChangedCallback(editDrop.selectedValueChanged))));

        private static void selectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            editDrop ed = d as editDrop;

            string val;

            if (e.NewValue == null)
                val = "";
            else
                val = e.NewValue.ToString();


            if (ed.comboBox1.Items.Count > 0)
                ed.comboBox1.SelectedValue = val;
            else
                ed.textBox1.Text = val;
        }

        public IEnumerable<string> dataSet
        {
            get { return (IEnumerable<string>)GetValue(dataSetProperty); }
            set { SetValue(dataSetProperty, value); }

        }
        public static readonly DependencyProperty dataSetProperty = DependencyProperty.Register("dataSet", typeof(IEnumerable<string>), typeof(editDrop),
        new PropertyMetadata((new PropertyChangedCallback(editDrop.dataSetValueChanged))));

        private static void dataSetValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            editDrop ed = d as editDrop;
            ed.dataSet = (IEnumerable<string>)e.NewValue;

            if (ed.dataSet.Count() == 0)
                ed.fillDrop();
            else
                ed.fillDrop(ed.dataSet);
        }

        public int length
        {
            get { return (int)GetValue(lengthProperty); }
            set { SetValue(lengthProperty, value); }
        }
        public static readonly DependencyProperty lengthProperty = DependencyProperty.Register("length", typeof(int), typeof(editDrop),
        new PropertyMetadata((new PropertyChangedCallback(editDrop.lengthChanged))));

        private static void lengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            editDrop ed = d as editDrop;

            ed.setLength(int.Parse(e.NewValue.ToString()));



        }



        public editDrop()
        {
            InitializeComponent();
            isNumeric = false;
            cf = new commonFuncs();
            FlipImage(image1);

        }
        public void setLength(int length)
        {
            textBox1.MaxLength = length;
        }
        public void fillDrop(IEnumerable<string> data)
        {
            comboBox1.ItemsSource = data;
            image1.Visibility = System.Windows.Visibility.Visible;
            comboBox1.Visibility = System.Windows.Visibility.Visible;
            textBox1.Visibility = System.Windows.Visibility.Collapsed;
            comboBox1.SelectedIndex = 0;

        }
        public void fillDrop(IEnumerable<int> data)
        {
            comboBox1.ItemsSource = data;
            image1.Visibility = System.Windows.Visibility.Visible;
            comboBox1.Visibility = System.Windows.Visibility.Visible;
        }
        public void fillDrop()
        {
            textBox1.Visibility = Visibility.Visible;
            image1.Visibility = System.Windows.Visibility.Collapsed;
            comboBox1.Visibility = System.Windows.Visibility.Collapsed;
            comboBox1.ItemsSource = null;
            textBox1.Focus();
        }
        public string getText()
        {
            return textBox1.Text;
        }
        public void clear()
        {
            textBox1.Text = "";
            if(comboBox1.Items.Count >0)
                comboBox1.SelectedIndex = 0;
            comboBox1.Visibility = Visibility.Visible;
            textBox1.Visibility = Visibility.Collapsed;
            image1.Visibility = System.Windows.Visibility.Visible;
        }
        public void setValue(string val)
        {
            isNumeric = false;
            comboBox1.SelectedValue = val;
            comboBox1.Visibility = Visibility.Visible;
            textBox1.Visibility = Visibility.Collapsed;

        }
        public void setValue(int val)
        {
            isNumeric = true;
            comboBox1.SelectedValue = val;
            comboBox1.Visibility = Visibility.Visible;
            textBox1.Visibility = Visibility.Collapsed;
        }
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comboBox1.Visibility == Visibility.Collapsed)
                setPullDown();
            else
                setTextBox();

        }
        private void setPullDown()
        {
            comboBox1.Visibility = Visibility.Visible;
            textBox1.Visibility = Visibility.Collapsed;
            //if (comboBox1.Items.Count > 0)    cant assume first element
            //    textBox1.Text = comboBox1.Items[0].ToString();
            //else
            //    textBox1.Text = "";
  
        }
        private void setTextBox()
        {
            comboBox1.Visibility = Visibility.Collapsed;



            textBox1.Visibility = Visibility.Visible;
            textBox1.Focus();
            textBox1.SelectAll();
            
        }



        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                textBox1.Text = comboBox1.SelectedValue.ToString();
                selectedValue = textBox1.Text;
            }
        }
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
        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            int xx;
            if (isNumeric)
            {
                if (int.TryParse(textBox1.Text, out xx) == false)
                {
                    MessageBox.Show("Value must be numeric", "Invalid input", MessageBoxButton.OK);
                    textBox1.Text = "";

                }
            }
            if (textBox1.Text == ""  && comboBox1.Items.Count > 0)
                setPullDown();

            textBox1.Background = new SolidColorBrush(Colors.White);

            selectedValue = textBox1.Text;

            flip.Stop();
        }
        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            Color c = cf.getRGBColor("FFA2B4C9");
            textBox1.Background = new SolidColorBrush(c);
           
            flip.Begin();
        }

        
    private void FlipImage(Image image)
    {




      // Create the storyboard.
      flip = new Storyboard();

      // Create animation and set the duration to 1 second.
      DoubleAnimation animation = new DoubleAnimation()
      {
        Duration = new TimeSpan(0, 0, 1),
        RepeatBehavior = new RepeatBehavior(1000)
      };

      // Add the animation to the storyboard.
      flip.Children.Add(animation);

      // Create a projection for the image if it doesn't have one.
      if (image.Projection == null)
      {
        // Set the center of rotation to -0.01, which will put a little space
        // between the images when they're flipped.
        image.Projection = new PlaneProjection()
        {
          CenterOfRotationX = .50 //-0.01
        };
      }

      PlaneProjection projection = image.Projection as PlaneProjection;
      animation.To =360;
       
      // Set the from and to properties based on the current flip direction of
      // the image.
      //if (projection.RotationY == 0)
      //{
      //  animation.To = 180;
      //}
      //else
      //{
      //  animation.From = 180;
      //  animation.To = 0;
      //}

      // Tell the animation to animation the image's PlaneProjection object.
      Storyboard.SetTarget(animation, projection);

      // Tell the animation to animation the RotationYProperty.
      Storyboard.SetTargetProperty(animation, 
        new PropertyPath(PlaneProjection.RotationYProperty));

      
    }

    private void textBox1_KeyUp(object sender, KeyEventArgs e)
    {
        selectedValue = textBox1.Text;
    }


  }





}
