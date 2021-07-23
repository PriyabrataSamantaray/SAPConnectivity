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

namespace slControls.metaGrid.interfaces
{
    public interface iReportmenu
    {
        void rMenu_showColumns(object sender, EventArgs e);
        void rMenu_exportClicked(object sender, EventArgs e);
        void rMenu_saveClicked(object sender, EventArgs e);
        void rMenu_deleteClicked(object sender, EventArgs e);
        //void cMenu_exportClicked(object sender, EventArgs e);
        //void cMenu_showColumnClicked(object sender, EventArgs e);
        //void cMenu_deleteClicked(object sender, EventArgs e);
        //void cMenu_saveClicked(object sender, EventArgs e);
        //void cMenu_prefsClicked(object sender, EventArgs e);
    }
}
