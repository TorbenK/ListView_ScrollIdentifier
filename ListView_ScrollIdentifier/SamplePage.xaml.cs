using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ListView_ScrollIdentifier
{
    public partial class SamplePage : ContentPage
    {
        public SamplePage()
        {
            InitializeComponent();

            this.BindingContext = new SampleViewModel();

            var dt = new DataTemplate(typeof(TextCell));
            this.list.ListView.ItemTemplate = dt;

            dt.SetBinding(TextCell.TextProperty, "Text");
        }
    }
}
