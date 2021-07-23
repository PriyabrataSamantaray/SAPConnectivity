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
using System.Collections;

namespace slControls.metaGrid.filters
{
    public class filterData :EventArgs
    {
        public IEnumerable data;

        public filterData(IEnumerable d)
        {
            data = d;
        }
    }
}
