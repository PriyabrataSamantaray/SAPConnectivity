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

namespace slControls.metaGrid.interfaces
{
    public interface iMetaColumn
    {
        string dataTemplateName { get; set; }
        string dataTemplateName_edit { get; set; }
        string header { get; set; }
        string colType { get; set; }
        bool isVisible { get; set; }
        bool isExported { get; set; }
        int width { get; set; }
        List<string> filter { get; set; }
    }
}
