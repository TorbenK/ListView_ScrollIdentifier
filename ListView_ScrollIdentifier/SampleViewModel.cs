using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListView_ScrollIdentifier
{
    public class SampleListViewItem
    {
        public string Text { get; set; }
    }

    public class SampleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<SampleListViewItem> _itemsSource;

        public ObservableCollection<SampleListViewItem> ItemsSource
        {
            get
            {
                return this._itemsSource;
            }
            set
            {
                if (this._itemsSource != value)
                {
                    this._itemsSource = value;
                    this.OnPropertyChanged("ItemsSource");
                }
            }
        }

        public SampleViewModel()
        {
            this._itemsSource = new ObservableCollection<SampleListViewItem>();
            for(int i = 1; i <= 50; i++)
            {
                this._itemsSource.Add(new SampleListViewItem { Text = string.Format("Item #{0}", i) });
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            var ev = this.PropertyChanged;

            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
