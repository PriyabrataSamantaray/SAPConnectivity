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
using System.Collections.Generic;

namespace slControls.certificates.viewModels
{
    public class certsViewModel : viewModelBase
    {


        private List<Visibility> _greenVisibility;
        public List<Visibility> greenVisibility { get { return _greenVisibility; } set { SetProperty(ref _greenVisibility, value, "greenVisibility"); } }

        private List<Visibility> _redVisibility;
        public List<Visibility> redVisibility { get { return _redVisibility; } set { SetProperty(ref _redVisibility, value, "redVisibility"); } }

        private List<Visibility> _yellowVisibility;
        public List<Visibility> yellowVisibility { get { return _yellowVisibility; } set { SetProperty(ref _yellowVisibility, value, "yellowVisibility"); } }

        private List<Visibility> _grayVisibility;
        public List<Visibility> grayVisibility { get { return _grayVisibility; } set { SetProperty(ref _grayVisibility, value, "grayVisibility"); } }

        private Visibility _layoutVisibility = Visibility.Collapsed;
        public Visibility layoutVisibility { get { return _layoutVisibility; } set { SetProperty(ref _layoutVisibility, value, "layoutVisibility"); } }


        private int stars = -1;
        private int totalStars;
        private bool employeeLacksCertificate;

        public certsViewModel()
        {
            resetStars();
        }

        public void resetStars()
        {
            List<Visibility> tmp1 = new List<Visibility>();
            List<Visibility> tmp2 = new List<Visibility>();
            List<Visibility> tmp3 = new List<Visibility>();
            List<Visibility> tmp4 = new List<Visibility>();

            for (int i = 0; i < 5; i++)
                tmp1.Add(Visibility.Collapsed);
            for (int i = 0; i < 5; i++)
                tmp2.Add(Visibility.Collapsed);
            for (int i = 0; i < 5; i++)
                tmp3.Add(Visibility.Collapsed);
            for (int i = 0; i < 5; i++)
                tmp4.Add(Visibility.Collapsed);


            greenVisibility = null;
            greenVisibility = tmp1;
            yellowVisibility = null;
            yellowVisibility = tmp2;
            redVisibility = null;
            redVisibility = tmp3;
            grayVisibility = null;
            grayVisibility = tmp4;


        }


        public void setJobLevel(string jobLevel)
        {
            layoutVisibility = Visibility.Collapsed;

            if(jobLevel == "")
                return;

            if (Convert.ToInt32(jobLevel) == 0)
                return;

            if (stars == -1)
                return;

            resetStars();
            totalStars = Convert.ToInt32(jobLevel) / 100;


            if (stars >= totalStars) //green
            {
                List<Visibility> tmp = greenVisibility;

                for (int i = 0; i < totalStars; i++)
                    tmp[i] = Visibility.Visible;

                greenVisibility = null;
                greenVisibility = tmp;
            }
            else
            {
                if (employeeLacksCertificate == false) //yellow
                {
                    List<Visibility> tmp1 = yellowVisibility;
                    List<Visibility> tmp2 = grayVisibility;

                    for (int i = 0; i < stars; i++)
                        tmp1[i] = Visibility.Visible;
                    for (int i = stars; i < totalStars; i++)
                        tmp2[i] = Visibility.Visible;


                    yellowVisibility = null;
                    yellowVisibility = tmp1;
                    grayVisibility = null;
                    grayVisibility = tmp2;

                }
                else
                {
                    List<Visibility> tmp3 = redVisibility;

                    for (int i = stars; i < totalStars; i++)
                        tmp3[i] = Visibility.Visible;

                    redVisibility = null;
                    redVisibility = tmp3;

                }



            }


            layoutVisibility = Visibility.Visible;
        }

        public void setEmployeeLevel(string empLevel)
        {

            layoutVisibility = Visibility.Collapsed;

            if (empLevel == "")
            {
                stars = 0;
                employeeLacksCertificate = true;
            }
            else
            {
                stars = Convert.ToInt32(empLevel) / 100;
                employeeLacksCertificate = false;
            }
        }

    }
}
