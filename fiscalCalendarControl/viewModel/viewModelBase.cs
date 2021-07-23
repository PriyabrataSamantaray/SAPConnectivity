using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace fiscalCalendar.viewModel
{
    public abstract class viewModelBase : INotifyPropertyChanged
    {
        public bool IsDesignTime
        {
            get { return DesignerProperties.IsInDesignTool; }
        }

        protected bool SetProperty<T>(ref T backingField, T value, string propertyName)
        {
            //var changed = !EqualityComparer<T>.Default.Equals(backingField, value);
            //if (changed)
            //{
            //    backingField = value;
            //    OnPropertyChanged(propertyName);
            //}
            //return changed;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            var propChanged = PropertyChanged;
            if (propChanged != null)
            {
                propChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
