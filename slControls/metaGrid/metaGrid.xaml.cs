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
using System.Collections;
using slControls.metaGrid.interfaces;
using System.Reflection;
using System.Text;
using System.IO;
using slControls.metaGrid.filters;
using System.ComponentModel;

namespace slControls.metaGrid
{
    public partial class metaGrid : UserControl, iReportmenu, iShowColumns, iSaveReport, iDeleteReport, iFilterWindow
    {
        private reportMenu rMenu;
        private showColumns columnsCwindow;
        private saveReport saveWindow;
        private deleteReport deleteWindow;
        private filterWindow displayFilterWindow;
        private filters.filterObj filter;
        private filters.filterHeader selectedFilter;
        public List<string> gapColumns = new List<string>();

        public event EventHandler saveClick;
        public event EventHandler deleteClick;
        public event EventHandler<filterData> filterReady; //not used
        public event EventHandler dataBack;
        public event EventHandler metaChanged;
        public event EventHandler customContextMenu;

        private bool newFilterObjectNeeded = true;



        public static readonly DependencyProperty GridSourceProperty = DependencyProperty.Register("GridSource", typeof(IEnumerable), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.GridSourceChanged))));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.ItemsSourceChanged))));

        public static readonly DependencyProperty parentResourcesProperty = DependencyProperty.Register("parentResources", typeof(ResourceDictionary), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.parentResourcesChanged))));

        public static readonly DependencyProperty metaDataProperty = DependencyProperty.Register("metaData", typeof(List<metaColumn>), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.metaDataPropertyChanged))));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.SelectedItemChanged))));

        public static readonly DependencyProperty SaveNamesProperty = DependencyProperty.Register("SaveNames", typeof(List<string>), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.SaveNamesChanged))));

        public static readonly DependencyProperty SelectedNameProperty = DependencyProperty.Register("SelectedName", typeof(string), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.SelectedNameChanged))));

        public static readonly DependencyProperty filtersProperty = DependencyProperty.Register("filters", typeof(Dictionary<string, List<string>>), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.filtersChanged))));

        public static readonly DependencyProperty SortableProperty = DependencyProperty.Register("Sortable", typeof(bool), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.SortableChanged))));

        public static readonly DependencyProperty saveMetaProperty = DependencyProperty.Register("saveMeta", typeof(bool), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.saveMetaChanged))));

        public static readonly DependencyProperty FrozenProperty = DependencyProperty.Register("Frozen", typeof(int), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.FrozenChanged))));


        public static readonly DependencyProperty SubtotalRowInfoProperty = DependencyProperty.Register("subtotalRowInfo", typeof(metaTotalRow), typeof(metaGrid),
        new PropertyMetadata((new PropertyChangedCallback(metaGrid.SubtotalRowInfoChanged))));

        public Dictionary<string, List<string>> filters
        {
            get { return (Dictionary<string, List<string>>)GetValue(filtersProperty); }
            set { SetValue(filtersProperty, value); }
        }

        public bool saveMeta
        {
            get { return (bool)GetValue(saveMetaProperty); }
            set { SetValue(saveMetaProperty, value); }
        }

        public string SelectedName
        {
            get { return (string)GetValue(SelectedNameProperty); }
            set { SetValue(SelectedNameProperty, value); }
        }

        public metaTotalRow subtotalRowInfo
        {
            get { return (metaTotalRow)GetValue(SubtotalRowInfoProperty); }
            set { SetValue(SubtotalRowInfoProperty, value); }
        }

        public bool Sortable
        {
            get { return (bool)GetValue(SortableProperty); }
            set { SetValue(SortableProperty, value); }
        }
        public int Frozen
        {
            get { return (int)GetValue(FrozenProperty); }
            set { SetValue(FrozenProperty, value); }
        }

        public List<string> SaveNames
        {
            get { return (List<string>)GetValue(SaveNamesProperty); }
            set { SetValue(SaveNamesProperty, value); }

        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }

        }
        public IEnumerable GridSource
        {
            get { return (IEnumerable)GetValue(GridSourceProperty); }
            set { SetValue(GridSourceProperty, value); }

        }
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }

        }

        public ResourceDictionary parentResources
        {
            get { return (ResourceDictionary)GetValue(parentResourcesProperty); }
            set { SetValue(parentResourcesProperty, value); }

        }

        public List<metaColumn> metaData
        {
            get { return (List<metaColumn>)GetValue(metaDataProperty); }
            set { 
                SetValue(metaDataProperty, value); 
            }

        }

        private static void SubtotalRowInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;
            pg.subtotalRowInfo = (metaTotalRow)e.NewValue;

        }
        private static void saveMetaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;

            if(pg.metaData != null)
                pg.refreshMetaDataFromGrid();
        }

        private static void SortableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;

            pg.myGrid.CanUserSortColumns = (bool)e.NewValue;
        }


        private static void FrozenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;

            pg.myGrid.FrozenColumnCount = (int)e.NewValue;
        }


        private static void filtersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;
            pg.filters = (Dictionary<string, List<string>>)e.NewValue;

            pg.newFilterObjectNeeded = true;
        }

        private static void metaDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;


            pg.metaData = (List<metaColumn>)e.NewValue;

            IEnumerable tmpSrc = pg.ItemsSource;
            pg.ItemsSource = null;



            pg.myGrid.Columns.Clear();





            if (pg.metaData == null)
                return;


            foreach (metaColumn col in pg.metaData)
            {
                if (col.isVisible)
                {
                    DataGridTemplateColumn dgtc = new DataGridTemplateColumn();
                    dgtc.CellTemplate = (DataTemplate)pg.parentResources[col.dataTemplateName];
                    dgtc.Width = new DataGridLength(col.width);
                    dgtc.Header = col.header;
                    dgtc.SortMemberPath = col.sortPath;// col.dataTemplateName;

                    if (col.dataTemplateName_edit != null)
                        dgtc.CellEditingTemplate = (DataTemplate)pg.parentResources[col.dataTemplateName_edit];

                    if (col.colType == "filterable")
                    {
                        if(pg.filters.ContainsKey(col.dataTemplateName))
                            dgtc.HeaderStyle = (Style)pg.Resources["filterStyleOn"];
                        else
                            dgtc.HeaderStyle = (Style)pg.Resources["filterStyleOff"];
                    }
                    else if (col.colType == "gap")
                    {
                        pg.gapColumns.Add(col.dataTemplateName);

                        if (pg.filters.ContainsKey(col.dataTemplateName))
                            dgtc.HeaderStyle = (Style)pg.Resources["gapStyleOn"];
                        else
                            dgtc.HeaderStyle = (Style)pg.Resources["gapStyleOff"];
                    }
                    else if(col.colType == "regular")
                        dgtc.HeaderStyle = (Style)pg.Resources["plainWrapHeader"];
                    else
                        dgtc.HeaderStyle = (Style)pg.parentResources[col.colType];

                    pg.myGrid.Columns.Add(dgtc);
                }
            }
           

            pg.ItemsSource = tmpSrc;

            pg.metaDataChanged();
        }

        private static void SelectedNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;
            pg.SelectedName = (string)e.NewValue;
        }


        private static void SaveNamesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;
            pg.SaveNames = (List<string>)e.NewValue;
        }
        private static void parentResourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;
            pg.parentResources = (ResourceDictionary)e.NewValue;

        }


        private static void GridSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;

            pg.GridSource = (IEnumerable)e.NewValue;

            pg.filter.gridData = pg.GridSource;
            pg.newFilterObjectNeeded = false;

        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            metaGrid pg = d as metaGrid;

            pg.ItemsSource = (IEnumerable)e.NewValue;


            if (pg.newFilterObjectNeeded)
            {

                if (pg.filters == null)
                    pg.filters = new Dictionary<string, List<string>>();

                if (pg.ItemsSource != null)
                {
                    pg.filter = new filters.filterObj(pg.ItemsSource, pg.filters, pg.subtotalRowInfo, pg.gapColumns);
                    pg.GridSource = pg.ItemsSource;
                }
  
            }

            

            pg.myGrid.ItemsSource = pg.filter.getData(pg.filters);

            //pg.GridSource = pg.myGrid.ItemsSource;


            pg.sourceChanged();

            pg.newFilterObjectNeeded = true;
        }
        private void metaDataChanged()
        {
            if(metaChanged != null)
                metaChanged(this, new EventArgs());
        }


        private void sourceChanged()
        {
            if(dataBack != null)
                dataBack(this, new EventArgs());
        }

        private static void SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            metaGrid pg = d as metaGrid;

            pg.SelectedItem = e.NewValue;

            pg.myGrid.SelectedItem = pg.SelectedItem;

        }
        private void gapHeaderClick(object sender, MouseButtonEventArgs e)
        {
            selectedFilter = (slControls.metaGrid.filters.filterHeader)sender;

            if (selectedFilter.hitImage == false)
                return;

            string binding = (from p in metaData where p.header == selectedFilter.content select p.dataTemplateName).First<string>();



            displayFilterWindow.setGraphList(filter.showFilter(binding), filter.getSelection(binding), binding);
            displayFilterWindow.Show();
        }


        private void filterHeaderClick(object sender, MouseButtonEventArgs e)
        {
            selectedFilter = (slControls.metaGrid.filters.filterHeader)sender;

            if (selectedFilter.hitImage == false)
                return;

            string binding = (from p in metaData where p.header == selectedFilter.content select p.dataTemplateName).First<string>();



            displayFilterWindow.setList(filter.showFilter(binding), filter.getSelection(binding),  binding);
            displayFilterWindow.Show();
        }
        public void filterGrid(object sender, EventArgs e)
        {
            newFilterObjectNeeded = false;

            IEnumerable tmp = filter.applyFilter(displayFilterWindow.bindingName, displayFilterWindow.filteredArrayData);

            if (filterReady != null)
                filterReady(this, new filterData(tmp));
            else
                ItemsSource = tmp;



           if (displayFilterWindow.imgStr == "Filter-Off.png")
               selectedFilter.filterState = false;
           else
               selectedFilter.filterState = true;



        }

        public metaGrid()
        {
            InitializeComponent();

            rMenu = new reportMenu(this);

            rMenu.Style = (Style)Resources["CommonDialog"];

            displayFilterWindow = new filterWindow(this);
            displayFilterWindow.Style = (Style)Resources["CommonDialog"];

            Sortable = true;
        }
        private void refreshMetaDataFromGrid()
        {
            List<metaColumn> newOrder = new List<metaColumn>();
            List<DataGridColumn> gridOrder = (from p in myGrid.Columns orderby p.DisplayIndex select p).ToList<DataGridColumn>();

            foreach (DataGridColumn dgc in gridOrder)
            {
                foreach (metaColumn col in metaData)
                {
                    if (dgc.Header.ToString() == col.header)
                    {
                        metaColumn c = new metaColumn();
                        c.dataTemplateName = col.dataTemplateName;
                        c.dataTemplateName_edit = col.dataTemplateName_edit;
                        c.header = col.header;
                        c.isExported = col.isExported;
                        c.width = (int)dgc.Width.Value;
                        c.isVisible = true;
                        c.colType = col.colType;
                        c.sortPath = col.sortPath;
                        newOrder.Add(c);

                    }
                }
            }
            newOrder.AddRange(from p in metaData where p.isVisible == false select p);

            metaData = newOrder;
        }
        public void rMenu_exportClicked(object sender, EventArgs e)
        {
            refreshMetaDataFromGrid();

            StringBuilder strBuilder = new StringBuilder();

            var colHeader = from p in metaData where p.isExported && p.isVisible select p;

            foreach (metaColumn col in colHeader)
                strBuilder.Append(col.header.Replace("\n", " ") + ",");

            strBuilder.Append("\r\n");


            foreach (object o in myGrid.ItemsSource)
            {
                foreach (metaColumn col in metaData)
                {
                    if (col.isVisible && col.isExported)
                    {
                        PropertyInfo pi = o.GetType().GetProperty(col.dataTemplateName);
                        if (pi != null)
                        {
                            if (pi.GetValue(o, null) == null)
                                strBuilder.Append(" ,");
                            else
                                strBuilder.Append(pi.GetValue(o, null).ToString().Replace(",", ";").Replace("\r", " ").Replace("\n", " ") + ",");
                        }
                    }
                   

                }
                strBuilder.Append("\r\n");

            }
            string dataStr = strBuilder.ToString();


            SaveFileDialog sfd = new SaveFileDialog()
            {
                DefaultExt = "csv",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1
            };
            if (sfd.ShowDialog() == true)
            {

                using (Stream stream = sfd.OpenFile())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataStr);
                        writer.Close();
                    }
                    stream.Close();
                }
            }



        }
        public void  rMenu_saveClicked(object sender, EventArgs e)
        {
            saveWindow = new saveReport(this);
            saveWindow.Style = (Style)Resources["CommonDialog"];
            saveWindow.Show();
        }
        public void rMenu_deleteClicked(object sender, EventArgs e)
        {
            deleteWindow = new deleteReport(this);
            deleteWindow.Style = (Style)Resources["CommonDialog"];
            deleteWindow.Show();
        }
        public void rMenu_showColumns(object sender, EventArgs e)
        {
            refreshMetaDataFromGrid();
            columnsCwindow = new showColumns(this);
            columnsCwindow.Style = (Style)Resources["CommonDialog"];
            columnsCwindow.Show();
        }
        public void metaDataChanged(List<metaColumn> data)
        {
            List<metaColumn> newOrder = new List<metaColumn>();
            newOrder = (from p in data where p.header != "" select p).ToList<metaColumn>();

            if ((from p in data where p.header == "" select p).Count() != 0)
            {
                metaColumn first = (from p in data where p.header == "" select p).First<metaColumn>();
                newOrder.Insert(0, first);
            }

            metaData = newOrder;

        }
        public void saveReport(object sender, EventArgs e)
        {

            if (saveClick is object)
            {
                refreshMetaDataFromGrid();
                saveClick(this, new EventArgs());
            }
        }
        public void deleteReport(object sender, EventArgs e)
        {
            if (deleteClick is object)
                deleteClick(this, new EventArgs());
        }
        private void myGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = myGrid.SelectedItem;
        }

        private void myGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (customContextMenu is object)
                customContextMenu(this, new EventArgs());
            else
                rMenu.Show();

            e.Handled = true;
        }
    }
}
