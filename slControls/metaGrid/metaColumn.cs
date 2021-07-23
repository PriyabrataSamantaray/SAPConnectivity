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
using slControls.metaGrid.interfaces;
using System.Collections.Generic;

namespace slControls
{
    public class metaColumn
    {
        public string dataTemplateName;
        public string dataTemplateName_edit;
        public string header;
        public string saveName;
        public int colOrder;
        public string colType;
        public bool isVisible;
        public bool isExported;
        public int width;
        public List<string> filter;
        public string sortPath;
        public string app;
       


        public metaColumn()
        {
    
        }

        public metaColumn(iMetaColumn col)
        {
            this.dataTemplateName = col.dataTemplateName;
            this.dataTemplateName_edit = col.dataTemplateName_edit;
            this.sortPath = col.dataTemplateName;
            this.header = col.header;
            this.width = col.width;
            this.isVisible = col.isVisible;
            this.isExported = col.isExported;
            this.colType = col.colType;
            this.filter = col.filter;
        }



    }
}
