using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListView_ScrollIdentifier
{
    public class ScrollIdenticatorListView : ContentView
    {
        private readonly RelativeLayout _relativeLayout;

        private readonly ListView _listView;

        private readonly Image _upImage;
        private readonly Image _downImage;

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<ScrollIdenticatorListView, IEnumerable>(i => i.ItemsSource, default(IEnumerable), BindingMode.OneWay, null, ItemsSourceChanged);

        public static readonly BindableProperty ShowScrollIdenticatorProperty = BindableProperty.Create<ScrollIdenticatorListView, bool>(i => i.ShowScrollIdenticator, true, BindingMode.TwoWay, null, ShowScrollIdenticatorChanged);

        public ListView ListView
        {
            get
            {
                return this._listView;
            }
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public bool ShowScrollIdenticator
        {
            get
            {
                return (bool)this.GetValue(ShowScrollIdenticatorProperty);
            }
            set
            {
                this.SetValue(ShowScrollIdenticatorProperty, value);
            }
        }

        public ScrollIdenticatorListView()
        {
            this._listView = new ListView();

            this._listView.ItemAppearing += _listView_ItemAppearing;
            this._listView.ItemDisappearing += _listView_ItemDisappearing;

            this._relativeLayout = new RelativeLayout();

            this._upImage = new Image 
            {
                Source = "ic_keyboard_arrow_up_white_24dp.png",
                IsVisible = true
            };
            this._downImage = new Image 
            {
                Source = "ic_keyboard_arrow_down_white_24dp.png",
                IsVisible = true
            };

            this._relativeLayout.Children.Add(
                this._listView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Bounds.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Bounds.Height;
                }));
            this._relativeLayout.Children.Add(this._upImage, Constraint.RelativeToParent((parent) => 
            {
                return parent.Bounds.Right - 20;
            }), 
            Constraint.RelativeToParent((parent) => 
            {
                return parent.Bounds.Top;
            }));
            this._relativeLayout.Children.Add(this._downImage, Constraint.RelativeToParent((parent) =>
            {
                return parent.Bounds.Right - 20;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Bounds.Bottom - 20;
            }));

            this.Content = this._relativeLayout;
        }

        void _listView_ItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            if (!this.ShowScrollIdenticator || this._listView.ItemsSource == null) return;

            var casted = this._listView.ItemsSource.Cast<object>();

            if (casted.Count() == 0) return;

            if (e.Item == casted.First())
            {
                this._upImage.IsVisible = true;
            }
            else if (e.Item == casted.Last())
            {
                this._downImage.IsVisible = true;
            }
        }

        void _listView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (!this.ShowScrollIdenticator || this._listView.ItemsSource == null) return;

            var casted = this._listView.ItemsSource.Cast<object>();

            if (casted.Count() == 0) return;

            if (e.Item == casted.First())
            {
                this._upImage.IsVisible = false;
            }
            else if (e.Item == casted.Last())
            {
                this._downImage.IsVisible = false;
            }
        }

        private static void ShowScrollIdenticatorChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            var view = obj as ScrollIdenticatorListView;

            if (view != null)
            {
                view._upImage.IsVisible = false;
                view._downImage.IsVisible = false;
            }
        }
        private static void ItemsSourceChanged(BindableObject obj, IEnumerable oldValue, IEnumerable newValue)
        {
            var view = obj as ScrollIdenticatorListView;

            if (view != null)
            {
                view._listView.ItemsSource = newValue;
            }
        }
    }
}
