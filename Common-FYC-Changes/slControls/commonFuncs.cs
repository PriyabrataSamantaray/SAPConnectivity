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
using System.ServiceModel;

namespace slControls
{
    public class commonFuncs : UserControl
    {
        private ScaleTransform wallStretch;
        private TranslateTransform wallTranslate;
        private TransformGroup tfGp;
        private Canvas wall;
        private Grid LayoutRoot;
        public EndpointAddress endpointAddress;
        public BasicHttpBinding binding;



        public void setLayout(Grid gd, Canvas cv)
        {
            wall = cv;
            LayoutRoot = gd;

            wallTranslate = new TranslateTransform();
            wallTranslate.SetValue(NameProperty, "wallTranslate");

            wallStretch = new ScaleTransform();
            wallStretch.SetValue(NameProperty, "wallStretch");
            wallStretch.CenterX = 0;
            wallStretch.CenterY = 0;

            tfGp = new TransformGroup();
            tfGp.Children.Add(wallTranslate);
            tfGp.Children.Add(wallStretch);


            wall.RenderTransform = tfGp;

            RowDefinition rd = new RowDefinition();
            LayoutRoot.RowDefinitions.Add(rd);

            LayoutRoot.Loaded += new RoutedEventHandler(LayoutRoot_Loaded);
            LayoutRoot.SizeChanged += new SizeChangedEventHandler(LayoutRoot_SizeChanged);


        }

        public void setEndpoint(string paramUrl, string service)
        {
            if (paramUrl.Contains("localhost"))
                paramUrl += @"/prm"; //dev doesnt have a special binding

            paramUrl += @"/services/" + service;



            binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxBufferSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;

            endpointAddress = new EndpointAddress(@"http://" + paramUrl);

        }

        public void setRFCEndpoint(string paramUrl)
        {
            if (paramUrl.Contains("localhost"))
                paramUrl += @"/rfcWebServices/rfcWS.asmx";
            else if (paramUrl.Contains("qa"))
                paramUrl = @"bi-qa/rfcwebservices/rfcWS.asmx";
            else
                paramUrl = @"bi/rfcwebservices/rfcWS.asmx";

            binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxBufferSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;

            endpointAddress = new EndpointAddress(@"http://" + paramUrl);

        }

        public void setRFCEndExternals(string paramUrl)
        {
            if (paramUrl.Contains("localhost"))
                paramUrl += @"/rfcWebServices/prmExternals.asmx";
            else if (paramUrl.Contains("qa"))
                paramUrl = @"bi-qa/rfcwebservices/prmExternals.asmx";
            else
                paramUrl = @"bi/rfcwebservices/prmExternals.asmx";

            binding = new System.ServiceModel.BasicHttpBinding();
            binding.MaxBufferSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;

            endpointAddress = new EndpointAddress(@"http://" + paramUrl);

        }


        public Color getRGBColor(string s)
        {
            byte a = System.Convert.ToByte(s.Substring(0, 2), 16);
            byte r = System.Convert.ToByte(s.Substring(2, 2), 16);
            byte g = System.Convert.ToByte(s.Substring(4, 2), 16);
            byte b = System.Convert.ToByte(s.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);



        }

        #region layout
        private  void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            Grid g = sender as Grid;
            sizer(g);
        }
        private void sizer(Grid g)
        {
            double canvasHeight = g.RowDefinitions[0].ActualHeight;
            double canvasWidth = g.ActualWidth;

            double scaleX = canvasWidth / wall.Width;
            double scaleY = canvasHeight / wall.Height;


            wallStretch.ScaleX = scaleX;
            wallStretch.ScaleY = scaleY;

            wallTranslate.X = (g.ActualWidth - wall.Width * scaleX) / 2;


            this.UpdateLayout();
        }
        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid g = sender as Grid;
            sizer(g);
        }
        #endregion
    }

}
