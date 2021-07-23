﻿using System;
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
    public interface iShowColumns
    {
        List<metaColumn> metaData { get; set; }
        void metaDataChanged(List<metaColumn> data);

    }
}
