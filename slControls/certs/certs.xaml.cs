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
using slControls.certificates.viewModels;

namespace slControls.certificates
{
    public partial class certs : UserControl
    {
        private certsViewModel model;

        public static readonly DependencyProperty jobLevelProperty = DependencyProperty.Register("jobLevel", typeof(string), typeof(certs),
        new PropertyMetadata((new PropertyChangedCallback(certs.jobLevelChanged))));

        public static readonly DependencyProperty empLevelProperty = DependencyProperty.Register("empLevel", typeof(string), typeof(certs),
        new PropertyMetadata((new PropertyChangedCallback(certs.empLevelChanged))));



        public string jobLevel
        {
            get { return (string)GetValue(jobLevelProperty); }
            set { SetValue(jobLevelProperty, value); }

        }

        public string empLevel
        {
            get { return (string)GetValue(empLevelProperty); }
            set { SetValue(empLevelProperty, value); }

        }

        private static void empLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

            certs c = d as certs;

            c.model.setEmployeeLevel(e.NewValue.ToString());

        }


        private static void jobLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;


            certs c = d as certs;

            c.model.setJobLevel(e.NewValue.ToString());

        }



        public certs()
        {
            InitializeComponent();

            model = (certsViewModel)Resources["ViewModel"];
        }
    }
}
